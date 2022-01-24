using ComeX.UserDatabaseAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUser([FromQuery] string username, [FromQuery] string password)
        {
            var result = await _userService.CreateUser(username, password);

            if (result.AddedUser is not null)
                return Ok();
            else
                return BadRequest(result.Reason);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromQuery] string username, [FromQuery] string password)
        {
            var result = await _userService.Login(username, password);

            if (result is not null)
                return Ok(result);
            else
                return Unauthorized();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUser([FromQuery] string username, [FromQuery] string password, [FromQuery] string newPassword)
        {
            var result = await _userService.UpdateUser(username, password, newPassword);

            if (result is not null)
                return Ok();
            else
                return Unauthorized();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteUser([FromQuery] string username, [FromQuery] string password)
        {
            var result = await _userService.DeleteUser(username, password);

            if (result)
                return Ok();
            else
                return Unauthorized();
        }
    }
}
