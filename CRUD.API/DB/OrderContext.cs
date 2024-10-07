using CRUD.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.DB;

public class OrderContext(DbContextOptions<OrderContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
}
