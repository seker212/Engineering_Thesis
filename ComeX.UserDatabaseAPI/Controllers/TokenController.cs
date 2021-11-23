using ComeX.UserDatabaseAPI.Models;
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
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _tokenService.Get();

            if (result is null)
                return BadRequest();
            else
                return Ok(result);
        }

        [HttpGet("{id:length(24)}", Name = "GetToken")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _tokenService.Get(id);

            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpGet("GetTokenInfo")]
        public async Task<IActionResult> GetTokenInfo(string tokenHash)
        {
            var result = await _tokenService.GetTokenInfo(tokenHash);

            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Token token)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _tokenService.Create(token);
            return Ok(result);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Token tokenIn)
        {
            var token = await _tokenService.Get(id);

            if (token is null)
                return NotFound();

            await _tokenService.Update(id, tokenIn);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var token = await _tokenService.Get(id);

            if (token is null)
                return NotFound();

            await _tokenService.Remove(token.Id);
            return Ok();
        }
    }
}
