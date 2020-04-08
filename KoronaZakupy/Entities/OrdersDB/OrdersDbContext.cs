using System;
using Microsoft.EntityFrameworkCore;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

        public DbSet<OrdersDB.User> Users {get; set;}

        public DbSet<Order> Orders {get; set;}

      //  public DbSet<UserOrder> UsersOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserOrder>()
                   .HasKey(uo => new { uo.UserId, uo.OrderId });

        }
    }
}
