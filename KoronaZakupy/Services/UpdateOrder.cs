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

        public async Task ConfirmFinishedOrder(long id) {
            var order = _ordersRepository.FindById(id).Result;

            if (order.user1Confirmed == false)
                order.user1Confirmed = true;
            else
                order.user2Confirmed = true;

            await _ordersRepository.UpdateOrderAsync(order);
            await _unitOfWork.CompleteAsync();

        }


        public async Task CancelConfirmationOfFinisedOrder(long id) {
            var order = _ordersRepository.FindById(id).Result;

            if (order.user1Confirmed == true)
                order.user1Confirmed = false;
            else
                order.user2Confirmed = false;

            await _ordersRepository.UpdateOrderAsync(order);
            await _unitOfWork.CompleteAsync();

        }


        public async Task<bool> DidBothUsersConfirmedFinishedOrder(long id) {

            var order = _ordersRepository.FindById(id).Result;
            if (order.user2Confirmed == true && order.user1Confirmed == true)
                return true;
            else
                return false;
        }


    }
}
