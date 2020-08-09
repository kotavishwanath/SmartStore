using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using smartStoreApi.Common;
using smartStoreApi.Models.Request;
using smartStoreApi.Models.Response;
using smartStoreApi.Properties;
using smartStoreApi.Services.Interfaces;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace smartStoreApi.Controllers
{
    [AllowAnonymous]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [Route(Routes.LoginControllerRoute)]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogger _logger;
        public ILoginService _loginService;
        public IUserService _userService;

        public LoginController(ILogger<LoginController> logger,
                               ILoginService loginService,
                               IUserService userService)
        {
            _logger = logger;
            _loginService = loginService;
            _userService = userService;
        }

        /// <summary>
        /// Validate username & password and returns generated token
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns>LoginResponse</returns>
        [HttpPost]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var userResponse = await _loginService.Authenticate(loginRequest);
                if (userResponse == null)
                {
                    return Ok(string.Format(ValidationMessages.NotFound, ValidationMessages.User));
                }
                else if (loginRequest.Password != userResponse.Password)
                {
                    return Ok(string.Format(ValidationMessages.Invalid, nameof(loginRequest.Password)));
                }
                else 
                {
                    return Ok(userResponse); 
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// save user
        /// </summary>
        /// <param name="userRequest"></param>
        /// <returns>success/failed msg</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [Route(Routes.SaveUser)]
        public async Task<IActionResult> SaveUser([FromBody] UserRequest userRequest)
        {
            try
            {
                return Ok(await _userService.SaveUserAsync(userRequest));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}