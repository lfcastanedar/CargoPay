using Domain.DTO;
using Domain.DTO.Payment;
using Infraestructure.Core.DTO;

namespace Application.Services.Interfaces;

public interface IPaymentService
{
    public Task<ResponseDto> PaymentAsync(PaymentAsyncRequest model, int UserId);
}