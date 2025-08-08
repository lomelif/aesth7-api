using Aesth.Application.Interfaces;
using Aesth.Application.UseCases;
using Aesth.Application.UseCases.Auth;
using Aesth.Application.UseCases.Checkout;
using Aesth.Application.UseCases.Order;
using Aesth.Infrastructure.Persistence.Repositories;
using Aesth.Infrastructure.Security;
using Aesth.Infrastructure.Services;
using Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://aesth7.com")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });

    options.AddPolicy("StripeWebhookPolicy", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddScoped<GetProductById>();
builder.Services.AddScoped<GetAllProducts>();
builder.Services.AddScoped<GetLatestProducts>();
builder.Services.AddScoped<GetTrendingProducts>();
builder.Services.AddScoped<GetCatalogProducts>();
builder.Services.AddScoped<CreateProduct>();
builder.Services.AddScoped<UpdateProduct>();
builder.Services.AddScoped<DeleteProduct>();
builder.Services.AddScoped<GetUserById>();
builder.Services.AddScoped<GetAllUsers>();
builder.Services.AddScoped<CreateUser>();
builder.Services.AddScoped<UpdateUser>();
builder.Services.AddScoped<DeleteUser>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<RegisterUseCase>();
builder.Services.AddScoped<CreateCheckoutSessionUseCase>();
builder.Services.AddScoped<CreateOrderUseCase>();
builder.Services.AddScoped<GetOrdersByEmailUseCase>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ICheckoutService, StripeCheckoutService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.Use(async (context, next) =>
{
    await next();
});

app.Map("/api/Order", webhookApp =>
{
    webhookApp.UseCors("StripeWebhookPolicy");
    
    webhookApp.Use(async (context, next) =>
    {
        await next();
    });
    
    webhookApp.UseEndpoints(endpoints => endpoints.MapControllers());
});

app.UseCors("FrontendPolicy");
app.MapControllers();

app.Run();