using Microsoft.AspNetCore.Mvc;
using Yapily.BO.Models;
using Yapily.Core.SDK.SDK.Users;

namespace YapilyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("users/create")]
        public async Task<IActionResult> CreateUser(string applicationUserId = null)
        {
            try
            {
                var user = await _userService.CreateUserAsync(applicationUserId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
