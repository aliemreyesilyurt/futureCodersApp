using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Identity;

namespace Services.Contracts
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> RegisterUser(UserForRegistirationDto userForRegistirationDto);
        Task<bool> ValidateUser(UserForAuthenticationDto userForAuthDto);
        Task<TokenDto> CreateToken(bool populateExp);
        Task<TokenDto> RefreshToken(TokenDto tokenDto);

        Task<(IEnumerable<UserDto> users, MetaData metaData)> GetAllUsersAsync(UserParameters userParameters);
        Task<UserDto> GetOneUserByIdAsync(string id);
        Task<bool> NewRandomPasswordAsync(UserForgetPasswordDto userDto);
        Task<bool> UpdateOneUserAsync(string id, UserDtoForUpdate userDto);
        Task<bool> UpdateOneUserPasswordAsync(string id, UserChangePasswordDto userDto);
        Task<bool> UpdateOneUserEmailAsync(string id, UserChangeEmailDto userDto);
        Task UpdateOneUserStatusAsync(string id);
        Task<bool> DeleteOneUserAsync(string id);
    }
}
