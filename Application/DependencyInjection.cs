using Domain.Services.Interfaces;
using Domain.Services.Interfaces.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Domain;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<ICardService, CardService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddSingleton<IUFEService, UFEService>();
        
       
    }
}