using Infraestructure.Data;
using Infraestructure.Repository;
using Infraestructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infraestructure;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        #region SQL Connection

        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringSQLServer"));
        });

        #endregion

        builder.Services.AddScoped<ICardRepository, CardRepository>();
    }
}