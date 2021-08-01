using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DTOs.Req;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IDbService _dbService;
        public static bool hasAccess = false;

        public AccountsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginController(LoginRequest request)
        {
            Tuple<bool, object> tuple = await _dbService.Login(request);
            if (tuple.Item1 == false)
                return Unauthorized();
            return Ok(tuple.Item2);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokenController([FromHeader(Name = "Authorization")] string token, RefreshToken refreshToken)
        {
            Tuple<bool, object> tuple = await _dbService.RefreshToken(token,refreshToken);
            if (tuple.Item1 == false)
                return Unauthorized();
            return Ok(tuple.Item2);          
        }

    }
}
