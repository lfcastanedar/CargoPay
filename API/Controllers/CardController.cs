using API.Handlers;
using Application.Services.Interfaces;
using Domain.DTO;
using Domain.DTO.Card;
using Domain.Helpers;
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


        [HttpPost("CreateCard")]
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
        
        [HttpGet("{cardNumber}/balance")]
        public async Task<IActionResult> GetCardBalance(string cardNumber)
        {
            string userId = Helper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.UserId);
            var result = await _cardService.GetCardBalanceAsync(cardNumber, Convert.ToInt32(userId));
            
            ResponseDto response = new ResponseDto
            {
                Message = "",
                Result = result,
                IsSuccess = true
            };
            
            return Ok(response);
        }
    }
}
