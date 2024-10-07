using CRUD.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.DB;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken = default);
    Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task UpdateOrderPriceAsync(Guid id, decimal price, CancellationToken cancellationToken = default);
}


public class OrderRepository(OrderContext orderContext) : IOrderRepository
{
    public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken = default)
    {
        return await orderContext.Orders.ToListAsync(cancellationToken);
    }

    public async Task<Order> GetOrderByIdAsync(Guid id, CancellationToken cancellationToken = default) => 
        await orderContext.Orders.FirstOrDefaultAsync(p => p.Id == id, cancellationToken) ?? throw new ArgumentException();

    public async Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        orderContext.Orders.Add(order);
        await orderContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateOrderPriceAsync(Guid id, decimal price, CancellationToken cancellationToken = default)
    {
        var order = await orderContext.Orders.FirstOrDefaultAsync(p => p.Id == id, cancellationToken) ?? throw new ArgumentException();
        order.Total = price;
        await orderContext.SaveChangesAsync(cancellationToken);
    }
}