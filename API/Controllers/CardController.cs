using API.Handlers;
using Application.Services.Interfaces;
using Domain.DTO;
using Domain.DTO.Card;
using Domain.Helpers;
using Domain.Resources;
using Infraestructure.Core.Constans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [TypeFilter(typeof(ExtractUserIdFilter))]
    public class CardController : BaseController
    {

        private readonly ICardService _cardService;
        

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }


        /// <summary>
        /// Creates a new card with a length of 15-digit number.
        /// </summary>
        /// <param name="model">The card creation request containing details such as card number and initial balance.</param>
        /// <returns>An IActionResult containing the result of the card creation or a relevant status message.</returns>
        [HttpPost("CreateCard")]
        [ProducesResponseType(typeof(GetResponseDTO<CardResponse>), 200)]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest model)
        {
            var result = await _cardService.CreateCardAsync(model, UserId);
            
            ResponseDto response = new ResponseDto
            {
                Message = "",
                Result = result,
                IsSuccess = true
            };
            
            return Ok(response);
        }

        /// <summary>
        /// Retrieves the balance of a specified card based on its card number.
        /// </summary>
        /// <param name="cardNumber">The card number whose balance is to be retrieved.</param>
        /// <returns>An IActionResult containing the balance information or a relevant status message.</returns>
        [HttpGet("{cardNumber}/balance")]
        public async Task<IActionResult> GetCardBalance(string cardNumber)
        {
            var result = await _cardService.GetCardBalanceAsync(cardNumber, UserId);
            
            ResponseDto response = new ResponseDto
            {
                Message = result is null ? GeneralMessages.CreditCardNoExist : "",
                Result = result,
                IsSuccess = result is not null
            };
            
            return Ok(response);
        }
    }
}
