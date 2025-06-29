using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using BP.Api.Service;
using BP.Extensions; // IContactService, ContactService burada tanımlıysa
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Logging; // Add this for logging extensions
using Microsoft.Extensions.Logging.Log4Net.AspNetCore;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true, reloadOnChange: true)
    .Build();

// Connection string test
string? connectionString = configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Connection string: {connectionString}");

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddLog4Net("log4net.config");
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Konfigürasyon nesnesini builder'a dahil et
builder.Configuration.AddConfiguration(configuration);

// Service registration
builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<BP.Validations.ContactValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

// Response Caching servisini ekle
builder.Services.AddResponseCaching();

// Scoped servis örneği
builder.Services.AddScoped<IContactService, ContactService>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Ortam kontrolü
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();

// UseRouting mutlaka Authorization’dan önce olmalı
app.UseRouting();

// Response Caching middleware
app.UseResponseCaching();

app.UseAuthorization();

app.MapControllers();

// HealthChecks endpoint
app.UseCustomHealthChecks();

app.Run();