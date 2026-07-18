using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using UEventoBackend.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Contexto de base de datos (SQL Server).
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var proveedorPermitidos = "_misOrigenesPermitidos";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: proveedorPermitidos,
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") // Cambia esto por tu URL de Angular
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// En desarrollo NO se redirige a HTTPS: la redirección rompe las llamadas
// CORS del frontend Angular hacia http://localhost:5109.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Aplica la política CORS definida arriba (necesaria para el frontend Angular).
app.UseCors(proveedorPermitidos);

app.UseAuthorization();

app.MapControllers();

app.Run();
