using kashop.bll.Service;
using kashop.dal.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using System.Security.Claims;

namespace kashop.pl.Areas.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public CheckoutsController(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Payment([FromBody]CheckoutRequest request)
        {
            var usreId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response=await _checkoutService.ProcessPaymentAsync(request,usreId);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("success")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromQuery]string session_id)
        {
            var response = await _checkoutService.HandelSuccessAsync(session_id);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
            
            
        }
    }
}
