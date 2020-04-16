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


        public async Task CreateOrderAsync(Order order, string userId)
        {
            
           var result = await _ordersDb.Orders.AddAsync(order);
            await AddRelationAsync(order, userId);   
        }

        public async Task AddRelationAsync(Order order, string userId)
        {
            var user = await _ordersDb.Users.FirstOrDefaultAsync(user => user.UserId == userId);

            var userOrder = new UserOrder()
            {
                User = user,
                Order = order,
                IsOrderConfirmed = false
            };

            await _ordersDb.AddAsync(userOrder);
        }

        public async Task AddRelationAsync(long orderId, string userId)
        {
            var user = await _ordersDb.Users.FirstOrDefaultAsync(user => user.UserId == userId);
            var order =   await _ordersDb.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
            order.IsActive = false;
            
            var userOrder = new UserOrder()
            {
                User = user,
                Order = order,
                IsOrderConfirmed = false
            };

            await _ordersDb.AddAsync(userOrder);
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


        public async Task<IEnumerable<Order>> ReadAllOrdersAsync()
        {
            return await _ordersDb.Orders.ToListAsync();
        }



        public async Task UpdateOrderAsync(Order order)
        {
            _ordersDb.Entry(order).State = EntityState.Modified;
            _ordersDb.Update(order);
        }

        public async Task UpdateUserOrderAsync(UserOrder userOrder)
        {
            _ordersDb.Entry(userOrder).State = EntityState.Modified;
            _ordersDb.Update(userOrder);
        }

        public async Task DeleteOrderAsync(long id)
        {
            // Not testing
            var order = await _ordersDb.Orders.FindAsync(id);
            var userOrder = (await _ordersDb.Orders.FindAsync(id)).Users.FirstOrDefault(uo => uo.OrderId == id);

            _ordersDb.Orders.Remove(order);
            _ordersDb.Remove(userOrder);
        }


        public async Task<bool> DoesIdExistAsync(long id)
        {
            return await _ordersDb.Orders.AnyAsync(order =>order.OrderId == id);
        }


        public async Task<Order> FindByIdAsync(long id) {
<<<<<<< HEAD
            return await _ordersDb.Orders.Include(o => o.Users).ThenInclude(row => row.User)
=======
            return  await _ordersDb.Orders.Include(o=> o.Users).ThenInclude(row => row.User)
>>>>>>> 7eff53ce19e44174804dd59b4eff10a352d6aa97
                .Where(order => order.OrderId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<OrderWithUsers>> FindByUserIdAsync(string userId)
        {
            var rawResult = await  FindByUserIdRaw(userId);

            var result =   rawResult.Select(order => new OrderWithUsers()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                IsFinished = order.IsFinished,
                UsersId = order.Users.Select(u => u.UserId).ToList()
            });

            return result;
        }

        private async Task <IEnumerable<Order>> FindByUserIdRaw(string userId) //return -record id- with userId in it
        {
            var result = _ordersDb.Orders.Include(order => order.Users)
            .ThenInclude(row => row.User).Where(o => o.Users.Any(uo => uo.UserId == userId));

            return result.AsEnumerable();
            
        }


        public async Task<IEnumerable<OrderWithUsers>> FindActiveOrdersByUserIdAsync(string userId) {

            var rawResult = await FindActiveOrdersByUserIdRaw(userId);

            var result =  rawResult.Select(order => new OrderWithUsers() {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                IsFinished = order.IsFinished,
                IsActive = order.IsActive,
                UsersId = order.Users.Select(u => u.UserId).ToList()
            });

            return result;
        }

        private async Task<IEnumerable<Order>> FindActiveOrdersByUserIdRaw(string userId)
        {
            var result = _ordersDb.Orders.Include(order => order.Users)
            .ThenInclude(row => row.User).Where(o => o.Users.Any(uo => uo.UserId == userId))
            .Where(x => x.IsActive == true);

            return result.ToList();

        }

        public async Task<IEnumerable<OrderWithUsers>> FindActiveUserAsync()
        {
            var rawResult = await FindActiveUserRawAsync();

            var result = rawResult.Select(order => new OrderWithUsers()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                IsFinished = order.IsFinished,
                IsActive = order.IsActive,
                UsersId = order.Users.Select(u => u.UserId).ToList()
            });

            return result;
        }

        private async Task<IEnumerable<Order>> FindActiveUserRawAsync()
        {
            return _ordersDb.Orders.Include(order => order.Users)
                 .ThenInclude(row => row.User).Where(order => order.IsActive == true);
        }


        public async Task<UserOrder> ConfirmOrder(long orderId, string userId)
        {
            var result = new UserOrder()
            {
                UserId = _ordersDb.Users.Where(user => user.UserId == userId).SingleOrDefault().UserId,
                OrderId = _ordersDb.Orders.Where(order => order.OrderId == orderId).SingleOrDefault().OrderId,
                IsOrderConfirmed = true
            };

            return result;
          
        }


        public async Task<UserOrder> CancelOfConfirmationOrder(long orderId, string userId)
        {
            var result = new UserOrder()
            {
                UserId = _ordersDb.Users.Where(user => user.UserId == userId).SingleOrDefault().UserId,
                OrderId = _ordersDb.Orders.Where(order => order.OrderId == orderId).SingleOrDefault().OrderId,
                IsOrderConfirmed = false
            };

            return result;

        }
    }
}
