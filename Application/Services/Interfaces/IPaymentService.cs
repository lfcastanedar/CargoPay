using Domain.DTO;
using Domain.DTO.Payment;

namespace Application.Services.Interfaces;

public interface IPaymentService
{
    public Task<ResponseDto> PaymentAsync(PaymentAsyncRequest model, int userId);
}