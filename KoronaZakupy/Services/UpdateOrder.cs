using System;
using System.Threading.Tasks;
using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Services.Interfaces;


namespace KoronaZakupy.Services {
    public class UpdateOrder : BaseOrderService, IUpdateOrder {
        public UpdateOrder(IOrdersRepository repo, IUnitOfWork unit) : base(repo, unit)
        {
            
        }
        //TODO
        //public async Task<IUpdateOrderResponse> FinishOrder(long id)
        public async Task FinishOrder(long id)
        {
            var order = _ordersRepository.FindById(id).Result;
            order.IsFinished = true;
            await _ordersRepository.UpdateOrderAsync(order);
            await _unitOfWork.CompleteAsync();
            
        }
    }
}
