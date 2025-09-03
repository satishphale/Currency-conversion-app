var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Auth setup (placeholder)
builder.Services.AddAuthentication("Bearer").AddJwtBearer();

// DI
builder.Services.AddHttpClient<IExchangeRateProvider, FrankfurterProvider>(c =>
{
    c.BaseAddress = new Uri("https://api.frankfurter.app/");
});
builder.Services.AddScoped<IExchangeService, ExchangeService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
