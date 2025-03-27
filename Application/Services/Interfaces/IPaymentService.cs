using Infraestructure.Core.DTO.Payment;

namespace Domain.Services.Interfaces.Interfaces;

public interface IPaymentService
{
    public Task<object> ProcessPaymentAsync(PayRequest model);
}