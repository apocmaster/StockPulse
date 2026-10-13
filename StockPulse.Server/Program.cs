using FluentValidation;
using Microsoft.EntityFrameworkCore;
using StockPulse.Server.Contexts;
using StockPulse.Server.Repositories;
using StockPulse.Server.Services;
using StockPulse.Server.Validators;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add configuration for the database context
builder.Services.AddDbContext<StockPulseDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("StockPulseConnection")));

// Add services to the container.
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Add validators to the container
builder.Services.AddScoped<IValidator<CreateProductDto>, CreateProductValidator>();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseDefaultFiles();
app.MapStaticAssets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
