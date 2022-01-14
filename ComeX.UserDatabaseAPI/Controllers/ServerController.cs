using ComeX.UserDatabaseAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComeX.UserDatabaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly IServerService _serverService;
        public ServerController(IServerService serverService)
        {
            _serverService = serverService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateServer([FromQuery] string name, [FromQuery] string url)
        {
            var result = await _serverService.CreateServer(name, url);

            if (result.AddedServer is not null)
                return Ok();
            else
                return BadRequest(result.Reason);
        }

        [HttpPost("user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddUserToServer([FromQuery] string username, [FromQuery] string url)
        {
            var result = await _serverService.AddUserToServer(username, url);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteServer([FromQuery] string url)
        {
            var result = await _serverService.DeleteServer(url);

            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetServers([FromQuery] string username)
        {
            var result = await _serverService.GetServers(username);

            if (result is not null)
                return Ok(result);
            else
                return BadRequest();
        }
    }
}
