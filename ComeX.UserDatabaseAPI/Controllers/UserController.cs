﻿using ComeX.UserDatabaseAPI.DAL;
using ComeX.UserDatabaseAPI.Models;
using ComeX.UserDatabaseAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _userService.Get();

            if (result is null)
                return BadRequest();
            else
                return Ok(result.ToList());
        }

        [HttpGet("{id:length(24)}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _userService.Get(id);

            if (result is null)
                return NotFound();
            else
                return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _userService.Create(user);
            return Ok(result);
        }

        [HttpPut("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(string id, User userIn)
        {
            var user = await _userService.Get(id);

            if (user is null)
                return NotFound();

            await _userService.Update(id, userIn);
            return Ok();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.Get(id);

            if (user is null)
                return NotFound();

            await _userService.Remove(user.Id);
            return Ok();
        }
    }
}
