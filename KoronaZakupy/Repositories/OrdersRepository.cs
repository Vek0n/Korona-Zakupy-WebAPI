using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace KoronaZakupy.Repositories {
    public class OrdersRepository : IOrdersRepository{

        private readonly OrdersDbContext _ordersDb;

        public OrdersRepository(OrdersDbContext ordersDb) {
            _ordersDb = ordersDb;
        }

        public async Task CreateUserAsync(User user)
        {
           await _ordersDb.Users.AddAsync(user);
        }

        public async Task CreateOrderAsync(Order order, string userId1, string userId2)
        {
            var user1 = await _ordersDb.Users.FirstOrDefaultAsync(user => user.UserId == userId1);
            var user2 = await _ordersDb.Users.FirstOrDefaultAsync(user => user.UserId == userId2);

            var userOrder1 = new UserOrder()
            {
                User = user1,
                Order = order
            };

            var userOrder2 = new UserOrder()
            {
                User = user2,
                Order = order
            };

            await _ordersDb.Orders.AddAsync(order);
            await _ordersDb.AddAsync(userOrder1);
            await _ordersDb.AddAsync(userOrder2);

        }

        public async Task<Order> ReadOrderAsync(long id)
        {
            if (!await _ordersDb.Orders.AnyAsync(o => o.OrderId == id))
                return null;

            var order = await _ordersDb.Orders.FindAsync(id);
            _ordersDb.Entry(order).State = EntityState.Detached;

            if (order == null)
            {
                return null;
            }

            return order;
        }
        public async Task<IEnumerable<Order>> ReadAllOrdersAsync(long id)
        {
            return await _ordersDb.Orders.ToListAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _ordersDb.Entry(order).State = EntityState.Modified;
            _ordersDb.Update(order);
        }

        public async Task DeleteOrderAsync(long id)
        {
            var order = await _ordersDb.Orders.FindAsync(id);
            // var userOrder = await _ordersDb.UsersOrders.FirstOrDefaultAsync(uo => uo.OrderId == id);
            var userOrder = (await _ordersDb.Orders.FindAsync(id)).Users.FirstOrDefault(uo => uo.OrderId == id);

            _ordersDb.Orders.Remove(order);
            _ordersDb.Remove(userOrder);
        }

        public async Task<bool> DoesIdExist(long id)
        {
            return await _ordersDb.Orders.AnyAsync(order =>order.OrderId == id);
        }
        public async Task<long> FindByUserId(string userId) //return -record id- with userId in it
        {
            return 22;
        }
    }
}
