using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using smartStoreApi.Common;
using smartStoreApi.Models.Response;
using smartStoreApi.Services.Interfaces;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace smartStoreApi.Controllers
{
    //[Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(string), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    [Route(Routes.UserControllerRoute)]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;
        public IUserService _userService;

        public UserController(ILogger<UserController> logger,
                               IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserProductResponse), StatusCodes.Status200OK)]
        [Route("getuserproducts/{userId}")]
        public async Task<IActionResult> GetUserProductsAsync(int userId)
        {
            try
            {
                return Ok(await _userService.GetUserProductsAsync(userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserProductResponse), StatusCodes.Status200OK)]
        [Route("getproductdetails/{productId}/{categoryId}/{userId}")]
        public async Task<IActionResult> GetProductDetailsAsync(int productId, int categoryId, int userId)
        {
            try
            {
                return Ok(await _userService.GetProductDetailsAsync(productId, categoryId, userId));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}