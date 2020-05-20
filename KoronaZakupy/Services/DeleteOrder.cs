using AutoMapper;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Repositories;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services
{
    public class DeleteOrder : BaseOrderService,IDeleteOrder
    {
        public DeleteOrder(IOrdersRepository repo, IUnitOfWork unit, IMapper mapper) : base(repo, unit, mapper)
        {

        }
        public async Task Delete(long orderId)
        {
            var order = await _ordersRepository.GetOrderEntityAsync(orderId);

            foreach (var user in order.Users)
            {
               await _ordersRepository.DeleteRelationAsync(orderId, user.UserId);
               await _unitOfWork.CompleteAsync();
            }

            await DeleteOrderAsync(order);
        }

        private async Task DeleteOrderAsync(Order order)
        {
            _ordersRepository.Delete(order);
            await _unitOfWork.CompleteAsync();
        }

    }
}
