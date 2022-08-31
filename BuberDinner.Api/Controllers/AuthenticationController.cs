using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            var authResult = _authenticationService.Register(registerRequest.FirstName,
                registerRequest.LastName,registerRequest.Email,registerRequest.Password);
            var response = new AuthenticationResponse(authResult.User.Id,
                authResult.User.FirstName,
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
            return Ok(response);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            var authResult = _authenticationService.Login(loginRequest.Email, loginRequest.Password);
            var response = new AuthenticationResponse(
                authResult.User.Id, 
                authResult.User.FirstName, 
                authResult.User.LastName,
                authResult.User.Email,
                authResult.Token);
            return Ok(response);
        }
    }
}
