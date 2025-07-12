using Aesth.Application.Interfaces;
using Aesth.Application.UseCases;
using Aesth.Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<GetProductById>();
builder.Services.AddScoped<GetAllProducts>();
builder.Services.AddScoped<CreateProduct>();
builder.Services.AddScoped<UpdateProduct>();
builder.Services.AddScoped<DeleteProduct>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.Run();