using API.Handlers;
using Domain.Services.Interfaces.Interfaces;
using Infraestructure.Core.Constans;
using Infraestructure.Core.DTO.Card;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Infraestructure.Core.DTO;
using Infraestructure.Core.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    [Authorize]
    public class CardController : ControllerBase
    {

        private readonly ICardService _cardService;
        

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }


        [HttpPost("CreateCard")]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardRequest model)
        {
            string userId = Helper.GetClaimValue(Request.Headers["Authorization"], TypeClaims.UserId);
            var result = await _cardService.CreateCardAsync(model, Convert.ToInt32(userId));
            
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
