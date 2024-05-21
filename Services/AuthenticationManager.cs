using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Exceptions;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthenticationManager : IAuthenticationService
    {
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private User? _user; //sadece bu sinif icerisinde kullanilacak

        public AuthenticationManager(ILoggerService logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<TokenDto> CreateToken(bool populateExp)
        {
            var signinCredentials = GetSiginCredentials(); //kimlik bilgilerini alma

            var claims = await GetClaims(); //rolleri alma

            var tokenOptions = GenerateTokenOptions(signinCredentials, claims); //token olusturma seceneklerini generate etme

            var refreshToken = GenerateRefreshToken();
            _user.RefreshToken = refreshToken;

            if (populateExp)
                _user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(_user); // veritabani guncelleme

            var accessToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<IdentityResult> RegisterUser(UserForRegistirationDto userForRegistirationDto)
        {
            var user = _mapper.Map<User>(userForRegistirationDto);

            var result = await _userManager
                .CreateAsync(user, userForRegistirationDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRolesAsync(user, userForRegistirationDto.Roles);
            }

            return result;
        }

        public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto)
        {
            _user = await _userManager.FindByNameAsync(userForAuthDto.UserName);

            var result = (_user != null && await _userManager.CheckPasswordAsync(_user, userForAuthDto.Password));

            if (!result)
            {
                _logger.LogWarning($"{nameof(ValidateUser)} : Authentication failed. Wrong username or password!");
            }
            return result;
        }

        public async Task<TokenDto> RefreshToken(TokenDto tokenDto)
        {
            var principal = GetPrincipalFromExpiredToken(tokenDto.AccessToken);
            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null ||
                user.RefreshToken != tokenDto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                throw new RefreshTokenBadRequestException();
            }

            _user = user;
            return await CreateToken(populateExp: false);
        }


        // Get-All
        public async Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParameters userParameters)
        {
            if (!userParameters.ValidRank)
                throw new InvalidRankBadRequestException();

            var usersQuery = _userManager.Users.AsQueryable();

            // Filtering
            if (userParameters.IsAvailable.HasValue)
                usersQuery = usersQuery.Where(u => u.IsAvailable == userParameters.IsAvailable);

            if (userParameters.RankId.HasValue && userParameters.ValidRank)
                usersQuery = usersQuery.Where(u => u.RankId == userParameters.RankId);


            // Searching
            if (!string.IsNullOrWhiteSpace(userParameters.SearchTerm))
            {
                usersQuery = usersQuery.Where(u => u.FirstName.Contains(userParameters.SearchTerm) ||
                                                   u.LastName.Contains(userParameters.SearchTerm));
            }

            var usersWithMetaData = PagedList<User>
                                    .ToPagedList(usersQuery,
                                        userParameters.PageNumber,
                                        userParameters.PageSize);

            var usersDto = _mapper.Map<IEnumerable<UserDto>>(usersWithMetaData);

            return (usersDto, usersWithMetaData.MetaData);
        }

        // Get-One
        public async Task<UserDto> GetOneUserByIdAsync(string id)
        {
            var _user = await GetOneUserByIdAndCheckExist(id);

            //mapping
            var userDto = _mapper.Map<UserDto>(_user);

            return userDto;
        }

        // Reset-Password-Link
        public async Task<bool> NewRandomPasswordAsync(UserForgetPasswordDto userDto)
        {
            var _user = await _userManager.FindByEmailAsync(userDto.Email);

            // Remove Current Password
            var removePasswordResult = await _userManager.RemovePasswordAsync(_user);

            if (!removePasswordResult.Succeeded)
                return false;

            var newPassword = GenerateRandomPassword(15);

            // Add Password
            var addPasswordResult = await _userManager.AddPasswordAsync(_user, newPassword);

            if (!addPasswordResult.Succeeded)
                return false;

            await SendEmailAsync(userDto.Email, $"Your new password is: {newPassword}");

            return true;
        }

        // Update-User
        public async Task<bool> UpdateOneUserAsync(string id, UserDtoForUpdate userDto)
        {
            var _user = await GetOneUserByIdAndCheckExist(id);

            _user.FirstName = userDto.FirstName;
            _user.LastName = userDto.LastName;

            var updateUserResult = await _userManager.UpdateAsync(_user);

            if (!updateUserResult.Succeeded)
                return false;

            return true;
        }

        // Update-Current Password
        public async Task<bool> UpdateOneUserPasswordAsync(string id, UserChangePasswordDto userDto)
        {
            var _user = await GetOneUserByIdAndCheckExist(id);

            var updatePasswordResult = await _userManager
                .ChangePasswordAsync(_user, userDto.CurrentPassword, userDto.NewPassword);

            if (!updatePasswordResult.Succeeded)
                return false;

            return true;
        }

        // Update-Email
        public async Task<bool> UpdateOneUserEmailAsync(string id, UserChangeEmailDto userDto)
        {
            var _user = await GetOneUserByIdAndCheckExist(id);

            var isCorrectPassword = await _userManager.CheckPasswordAsync(_user, userDto.Password);

            if (!isCorrectPassword)
                return false;

            _user.Email = userDto.NewEmail;

            var updateUserResult = await _userManager.UpdateAsync(_user);

            if (!updateUserResult.Succeeded)
                return false;

            return true;
        }

        // Update-Status
        public async Task UpdateOneUserStatusAsync(string id)
        {
            var _user = await GetOneUserByIdAndCheckExist(id);

            if (_user.IsAvailable == false)
                _user.IsAvailable = true;
        }

        // Delete-User
        public async Task<bool> DeleteOneUserAsync(string id)
        {
            var _user = await GetOneUserByIdAndCheckExist(id);

            var deleteUserResult = await _userManager
                .DeleteAsync(_user);

            if (!deleteUserResult.Succeeded)
                return false;

            return true;
        }


        // Yardimci metodlar
        private SigningCredentials GetSiginCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, _user.UserName),
                new Claim(ClaimTypes.NameIdentifier, _user.Id)

            };

            var roles = await _userManager
                .GetRolesAsync(_user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signinCredentials,
            List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signinCredentials);
            return tokenOptions;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];

            // using kullanimi, maaliyetli bir is yapilacagi zaman kullanilir, scope icerisindeki islemler bittigi zaman, score icerisinde kullanilan kaynaklar serbest birakilir!
            using (var rng = RandomNumberGenerator.Create()) //rng: random number generator
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        // GetPrincipalFromExpiredToken metodu parametre olarak gelen tokeni, bu gecerli bir token mi? bizim tarafimizdan mi uretildi? seklinde validate eder ve en sonunda kullanici bilgilerini alir
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["secretKey"];

            var tokenValidationParameters = new TokenValidationParameters
            {
                // Token dogrulamak icin parametreleri girme
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true, // gecerlilik suresi dogrula
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"], //gecerli bir uretici mi dogrula
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            // principal ile kullanici bilgileri alinir
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
                out securityToken); // out securityToken ifadesi eklenerek (ValidateToken metodu calistigi zaman) ust kisimda tanimlanan ve deger atamasi yapilmayan securityToken degiskenine bu metoddan donen sonuc set edilir!

            var jwtSecurityToken = securityToken as JwtSecurityToken; //cast islemi yapildi
            // eger ki bu cevirme basarili olursa referans tutar, basarisiz olursa null atanir
            if (jwtSecurityToken is null ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;

        }

        // Check
        private async Task<User> GetOneUserByIdAndCheckExist(string id)
        {
            //check entity
            var entity = await _userManager.FindByIdAsync(id);

            //check
            if (entity is null)
                throw new UserIdNotFoundException(id);

            return entity;
        }

        // Random Password
        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*-_=+;:',.<>?";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                                        .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        // Send-Email
        private async Task SendEmailAsync(string email, string message)
        {
            var smtpClient = new SmtpClient("smtp-mail.outlook.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("futurecoderswebapi@outlook.com", "Futurecoders1."),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("futurecoderswebapi@outlook.com"),
                Subject = "Your New Password",
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }

    }
}
