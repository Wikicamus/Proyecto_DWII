using SaleService.Domain.Interfaces;
using SaleService.Infrastructure.Data;
using SaleService.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using SaleService.Api.Feature.Sales.Commands;
using SaleService.Api.Feature.Sales.Query;
using SaleService.Api.Common;

var builder = WebApplication.CreateBuilder(args);

// Configuración de User Secrets (solo en Desarrollo)
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddAuthorization();

// Agregar HttpContextAccessor
builder.Services.AddHttpContextAccessor();

// Configuración de MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Configuración de la base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<db_Context>(options =>
    options.UseMySQL(connectionString ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Configurar repositorios genéricos
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
