using API.Handlers;
using Application.Services.Interfaces;
using Domain.DTO.Payment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(ExtractUserIdFilter))]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        
        [HttpPost("Pay")]
        public async Task<IActionResult> Pay([FromBody] PaymentAsyncRequest model)
        {
            var result = await _paymentService.PaymentAsync(model, UserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
