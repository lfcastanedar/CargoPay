using API.Handlers;
using Domain;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((hostContext, services, configuration) =>
{
    configuration.WriteTo.Console();
    configuration.MinimumLevel.Error();
    configuration.WriteTo.File("Logs/CargoPay.txt", rollingInterval: RollingInterval.Day);

});

#region SQL Connection
/*builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringSQLServer"));
});*/
#endregion


#region Inyection
//DependencyInyectionHandler.DependencyInyectionConfig(builder.Services);
builder.AddApplicationServices();
#endregion

#region JWT
var tokenAppSetting = builder.Configuration.GetSection("JWT");
JwtConfigurationHandler.ConfigureJwtAuthentication(builder.Services, tokenAppSetting);
#endregion


builder.Services.AddCors(Options =>
{
    Options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();

    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();