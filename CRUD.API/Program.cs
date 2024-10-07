using CRUD.API.DB;
using CRUD.API.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("Postgress")));
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/orders", async (IOrderRepository repository, CancellationToken cancellationToken) => await repository.GetOrdersAsync(cancellationToken))
.WithName("Get orders")
.WithOpenApi();

app.MapGet("/orders/{id}", async (Guid id, IOrderRepository repository, CancellationToken cancellationToken) => await repository.GetOrderByIdAsync(id, cancellationToken))
.WithName("Get order by id")
.WithOpenApi();

app.MapPost("/orders", async (Order order, IOrderRepository repository, CancellationToken cancellationToken) => await repository.AddOrderAsync(order, cancellationToken))
.WithName("Create order")
.WithOpenApi();

app.MapPatch("/orders/{id}", async (Guid id, decimal price, IOrderRepository repository, CancellationToken cancellationToken) => await repository.UpdateOrderPriceAsync(id, price))
.WithName("Update order price")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
