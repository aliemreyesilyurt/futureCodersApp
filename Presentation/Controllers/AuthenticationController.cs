﻿using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;

namespace Presentation.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public AuthenticationController(IServiceManager manager)
        {
            _manager = manager;
        }

        // Register
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistirationDto userForRegistirationDto)
        {
            var result = await _manager
                .AuthenticationService
                .RegisterUser(userForRegistirationDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }


        // Login
        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto user)
        {
            if (!await _manager.AuthenticationService.ValidateUser(user))
                return Unauthorized(); // 401

            var tokenDto = await _manager
                .AuthenticationService
                .CreateToken(populateExp: true);

            return Ok(tokenDto);
        }

        [HttpPost("refresh")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
        {
            var tokenDtoToReturn = await _manager
                .AuthenticationService
                .RefreshToken(tokenDto);

            return Ok(tokenDtoToReturn);
        }
    }
}
