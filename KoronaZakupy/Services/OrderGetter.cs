using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using System;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;
using KoronaZakupy.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using KoronaZakupy.Entities;
using AutoMapper;

namespace KoronaZakupy.Services {
    public class OrderGetter : BaseOrderService, IOrderGetter {

        private ICompleteUserInfo _completeUserInfo;
        public OrderGetter(IOrdersRepository repo, IUnitOfWork unit,
            ICompleteUserInfo completeUserInfo, IMapper mapper) : base(repo, unit, mapper) {

            _completeUserInfo = completeUserInfo;
        }

        public async Task<IEnumerable<OrderWithUsersInfo>> GetActiveOrdersAsync()
        {

            return await  _completeUserInfo.CompleteAsync( await _ordersRepository.FindActiveOrdersAsync() );
        }

        public async Task<IEnumerable<OrderWithUsersInfo>> GetOrdersAsync(string userId) {

            return await _completeUserInfo.CompleteAsync( await _ordersRepository.FindOrdersByUserIdAsync(userId) );
        }


        public async Task<IEnumerable<OrderWithUsersInfo>> GetUserActiveOrdersAsync(string userId) {

            return await _completeUserInfo.CompleteAsync(await _ordersRepository.FindOrdersByUserIdAsync(userId,true));
        }

    }
}
