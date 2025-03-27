using Domain.Services.Interfaces.Interfaces;
using Infraestructure.Core.DTO.Payment;

namespace Domain.Services.Interfaces;

public class PaymentService : IPaymentService
{
    public async Task<object> ProcessPaymentAsync(PayRequest model)
    {
        return 1;
    }
}