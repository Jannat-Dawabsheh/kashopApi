using kashop.bll.Service;
using kashop.dal.DTO.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace kashop.pl.Areas.Identity
{
    [Route("api/auth/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authinticationService;

        public AccountController(IAuthenticationService authinticationService)
        {
            _authinticationService = authinticationService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authinticationService.LoginAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPost("Register")]
        public async Task<IActionResult>Register(RegisterRequest request)
        {
            var result=await _authinticationService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token,string userId)
        {
            var result = await _authinticationService.ConfirmEmailAsync(token,userId);
           
            return Ok(result);
        }
        [HttpPost("SendCode")]
        public async Task<IActionResult>RequestPasswordReset(ForgotPasswordRequest request)
        {
            var result = await _authinticationService.RequestPasswordReset(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpPatch("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _authinticationService.ResetPassword(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPatch("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenApiModel request)
        {
            var result = await _authinticationService.RefreshTokenAsync(request);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }



    }
}
