using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Repositories {
    public class OrdersRepository : IOrdersRepository{

        private readonly OrdersDbContext _ordersDb;

        public OrdersRepository(OrdersDbContext ordersDb) {
            _ordersDb = ordersDb;
        }

        public async Task CreateAsync<T>(T resource, string userId ="") 
        {
                await _ordersDb.AddAsync(resource);

            if (resource is Order)
                await AddRelationAsync( (resource as Order).OrderId, userId);
        }

        public async Task AddRelationAsync(long orderId, string userId)
        {
            if (!await _ordersDb.Orders.AnyAsync())
                orderId = 1;
            else if (orderId == 0 || orderId == null)
                orderId = await GetNewId();
           
            var userOrder = new UserOrder()
            {
                UserId = userId,
                OrderId = orderId,
                IsOrderConfirmed = false
            };

            await _ordersDb.AddAsync(userOrder);
        }

        private async Task<long> GetNewId()
        {
            return  (await _ordersDb.Orders.OrderByDescending(order => order.OrderId).FirstOrDefaultAsync()).OrderId + 1;
        }


        public async Task DeleteRelationAsync(long orderId, string userId)
        {
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var removeUserOrder = new UserOrder()
            {
                UserId = userId,
                OrderId = orderId,
                IsOrderConfirmed = (await GetIsOrderConfirmed(orderId,userId))
            };
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            _ordersDb.Remove(removeUserOrder);
        }

        public async Task UpdateAsync<T>(T resource)
        {
            try
            {
                _ordersDb.Entry(resource).State = EntityState.Modified;//????
                _ordersDb.Update(resource);
            }
            catch(Exception ex)
            {
                var xe = ex.Message;
            }
        }

        public async Task<UserOrder> ChangeConfirmationOfOrderAsync(long orderId, string userId)
        {
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var result = new UserOrder()
            {
                UserId = userId,
                OrderId = orderId,
                IsOrderConfirmed = !(await GetIsOrderConfirmed(orderId, userId))
            };
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            return result;
        }

        private async Task<bool> GetIsOrderConfirmed(long orderId, string userId)
        {
            return (await _ordersDb.Orders.Include(order => order.Users)
                 .ThenInclude(row => row.User)
                 .Where(order => order.OrderId == orderId).SingleOrDefaultAsync()
                 ).Users.SingleOrDefault(uo => uo.OrderId == orderId && uo.UserId == userId).IsOrderConfirmed;
        }

         
        public async Task<Order> FindOrderByOrderIdAsync(long id) {

            return  await _ordersDb.Orders.Include(o=> o.Users).ThenInclude(row => row.User)
                .Where(order => order.OrderId == id).FirstOrDefaultAsync();
        }


        public async Task<IEnumerable<OrderWithUsersId>> FindOrdersByUserIdAsync(string userId, bool findByActivity=false)
        {
            var rawResult = await FindByUserIdRawAsync(userId, findByActivity);

            var result = rawResult.Select(order => new OrderWithUsersId()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Products = order.Products,
                IsFinished = order.IsFinished,
                IsActive = order.IsActive,
                UsersId = order.Users.Select(u => u.UserId).ToList()
            });

            return result;
        }

        private async Task<IEnumerable<Order>> FindByUserIdRawAsync(string userId, bool findByActivity = false)
        {
            if (!findByActivity) {
                return ( _ordersDb.Orders.Include(order => order.Users)
               .ThenInclude(row => row.User).Where(o => o.Users.Any(uo => uo.UserId == userId)) ).AsEnumerable();
            }

            return ( _ordersDb.Orders.Include(order => order.Users)
           .ThenInclude(row => row.User).Where(o => o.Users.Any(uo => uo.UserId == userId))
           .Where(x => x.IsActive == true) ).AsEnumerable();

        
        }

        public async Task<IEnumerable<OrderWithUsersId>> FindActiveOrdersAsync()
        {
            var rawResult = await FindActiveOrdersRawAsync();

            var result = rawResult.Select(order => new OrderWithUsersId()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                IsFinished = order.IsFinished,
                IsActive = order.IsActive,
                UsersId = order.Users.Select(u => u.UserId).ToList()
              
            });

            return result;
        }

        private async Task<IEnumerable<Order>> FindActiveOrdersRawAsync()
        {
            return _ordersDb.Orders.Include(order => order.Users)
                 .ThenInclude(row => row.User).Where(order => order.IsActive == true);
        }

    }
}
