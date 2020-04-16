using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using System;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;
using KoronaZakupy.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace KoronaZakupy.Services {
    public class OrderGetter : BaseOrderService, IOrderGetter {

        public OrderGetter(IOrdersRepository repo, IUnitOfWork unit) : base(repo, unit) {

        }

        public async Task<IEnumerable<OrderWithUsers>> GetActiveOrdersAsync()
        {

            return await _ordersRepository.FindActiveUserAsync();
        }

        public async Task<IEnumerable<OrderWithUsers>> GetOrdersAsync(string userId) {

            return await _ordersRepository.FindByUserIdAsync(userId);
        }


        public async Task<IEnumerable<OrderWithUsers>> GetUserActiveOrdersAsync(string userId) {

            return await _ordersRepository.FindActiveOrdersByUserIdAsync(userId);
        }

    }
}
