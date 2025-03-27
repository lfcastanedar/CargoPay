using API.Handlers;
using API.Handlers;
using Domain.Services.Interfaces.Interfaces;
using Infraestructure.Core.DTO;
using Infraestructure.Core.DTO.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(CustomExceptionHandler))]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] AuthRequest request)
        {
            var result = await _authService.Login(request);
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
