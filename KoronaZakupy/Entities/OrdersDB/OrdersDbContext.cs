using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using KoronaZakupy.Entities.OrdersDB;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

        public DbSet<OrdersDB.User> Users {get; set;}

        public DbSet<Order> Orders {get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserOrder>()
                .HasKey(uo => new { uo.UserId, uo.OrderId });

            var converter = new ValueConverter<IEnumerable<string>, string>(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<IEnumerable<string>>(v) ?? new List<string>());

            var comparer = new ValueComparer<IEnumerable<string>>(
                (l, r) => JsonConvert.SerializeObject(l) == JsonConvert.SerializeObject(r),
                v => v == null ? 0 : JsonConvert.SerializeObject(v).GetHashCode(),
                v => JsonConvert.DeserializeObject<IEnumerable<string>>(JsonConvert.SerializeObject(v)));

            builder.Entity<Order>()
                .Property(order => order.Products)
                .HasConversion(converter)
                .Metadata.SetValueConverter(converter);

            builder.Entity<Order>()
                .Property(order => order.Products)
                .Metadata.SetValueComparer(comparer);

            builder.Entity<Order>()
                .Property(order => order.OrderStatus)
                .HasConversion(
                    status => status.ToString(),
                    status => (Order.OrderStatusEnum) Enum.Parse(typeof(Order.OrderStatusEnum),status));

            //builder.Entity<Order>()
            //    .Property(order => order.OrderType)
            //    .HasConversion(
            //        orderType => orderType.ToString(),
            //        orderType => (Order.OrderTypeEnum) Enum.Parse(typeof(Order.OrderTypeEnum), orderType));
        }
    }
}
