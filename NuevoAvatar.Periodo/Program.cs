using NuevoAvatar.Periodo;
using NuevoAvatar.Periodo.Repository;
using NuevoAvatar.Periodo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
builder.Services.AddScoped<PeriodoRepository>();
builder.Services.AddScoped<IPeriodoService, PeriodoService>();
builder.Services.AddScoped<PeriodoValidator>();

var app = builder.Build();

if (app.Environment.IsDevelopment() ||
    app.Environment.IsStaging() ||
    app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapPeriodoEndpoints();

app.Run();