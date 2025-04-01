using API.Handlers;
using Application.Services.Interfaces;
using Domain.DTO;
using Domain.DTO.Card;
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

        /// <summary>
        /// Process a payment and charge payment fees based on UFE values.
        /// </summary>
        /// <param name="model">The payment details, including the card number and amount, encapsulated in a PaymentAsyncRequest object.</param>
        /// <returns>Returns the final value and balance account.
        /// </returns>
        [HttpPost("Pay")]
        [ProducesResponseType(typeof(PayResponseDto), 200)]
        public async Task<IActionResult> Pay([FromBody] PaymentAsyncRequest model)
        {
            var result = await _paymentService.PaymentAsync(model, UserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
