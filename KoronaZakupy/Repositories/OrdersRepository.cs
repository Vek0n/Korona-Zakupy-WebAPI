using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using KoronaZakupy.Entities;
using AutoMapper;

namespace KoronaZakupy.Repositories {
    public class OrdersRepository : IOrdersRepository{

        private readonly OrdersDbContext _ordersDb;
        private readonly IMapper _mapper;

        public OrdersRepository(OrdersDbContext ordersDb, IMapper mapper) {
            _ordersDb = ordersDb;
            _mapper = mapper;
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
           
            await _ordersDb.AddAsync(new UserOrder(orderId,userId) );
        }

        private async Task<long> GetNewId()
        {
            return  (await _ordersDb.Orders.OrderByDescending(order => order.OrderId).FirstOrDefaultAsync()).OrderId + 1;
        }


        public async Task DeleteRelationAsync(long orderId, string userId)
        {
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var isOrderConfirmed = (await GetIsOrderConfirmed(orderId, userId));
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            _ordersDb.Remove(new UserOrder(orderId,userId,isOrderConfirmed));
        }

        public async Task UpdateAsync<T>(T resource)
        {
                _ordersDb.Entry(resource).State = EntityState.Modified;
                _ordersDb.Update(resource); 
        }

        public async Task<UserOrder> ChangeConfirmationOfOrderAsync(long orderId, string userId)
        {
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            var isOrderConfirmed = !(await GetIsOrderConfirmed(orderId, userId));
            _ordersDb.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

            return new UserOrder(orderId,userId,isOrderConfirmed);
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

        public async Task<IEnumerable<OrderDTO>> FindOrdersByUserIdAsync(string userId, bool findByActivity=false)
        {
            var rawResult = await FindByUserIdRawAsync(userId, findByActivity);

            return rawResult.Select(order => _mapper.Map<OrderDTO>(order) );
        }

        private async Task<IEnumerable<Order>> FindByUserIdRawAsync(string userId, bool findByActivity = false)
        {
           
                if (!findByActivity)
                {
                    return (_ordersDb.Orders.Include(order => order.Users)
                        .ThenInclude(row => row.User).Where(o => o.Users.Any(uo => uo.UserId == userId))).AsEnumerable();
                }

                return (_ordersDb.Orders.Include(order => order.Users)
                    .ThenInclude(row => row.User).Where(o => o.Users.Any(uo => uo.UserId == userId))
                    .Where(x => x.OrderStatus == Order.OrderStatusEnum.Avalible)).AsEnumerable();
        }
        
        public async Task<IEnumerable<OrderDTO>> FindActiveOrdersAsync()
        {
            var rawResult = await FindActiveOrdersRawAsync();

            return rawResult.Select(order => _mapper.Map<OrderDTO>(order));
        }

        private async Task<IEnumerable<Order>> FindActiveOrdersRawAsync()
        {
            return _ordersDb.Orders.Include(order => order.Users)
                 .ThenInclude(row => row.User).Where(order => order.OrderStatus == Order.OrderStatusEnum.Avalible);
        }

    }
}
