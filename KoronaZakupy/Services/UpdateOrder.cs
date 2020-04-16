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
       
        public async Task FinishOrder(long id)
        {
            var order = await _ordersRepository.FindByIdAsync(id);
            order.IsFinished = true;

            await FinishUpdate(order);
            
        }

        public async Task ChangeActiveProperty(long id)
        {
            var order = await _ordersRepository.FindByIdAsync(id);

            if (order.IsActive)
                order.IsActive = false;
            else
                order.IsActive = true;

            await FinishUpdate(order);
        }

        public async Task ConfirmFinishedOrder(long id, string userId)
        {

            var userOrder = await _ordersRepository.ConfirmOrder(id, userId);
           
            await FinishUpdate(userOrder);

        }

        public async Task CancelConfirmationOfFinisedOrder(long id, string userId)
        {
            var userOrder = await _ordersRepository.CancelOfConfirmationOrder(id, userId);

            await FinishUpdate(userOrder);

        }

        private async Task FinishUpdate<T>(T resource)
        {
            if (resource is Order)
                await _ordersRepository.UpdateOrderAsync(resource as Order);
            if (resource is UserOrder)
                await _ordersRepository.UpdateUserOrderAsync(resource as UserOrder);

            await _unitOfWork.CompleteAsync();
        }


        public async Task<bool> DidBothUsersConfirmedFinishedOrder(long id)
        {

            var order = await _ordersRepository.FindByIdAsync(id);
            
            foreach( var userOrderRelation in order.Users)
            {
                if (!userOrderRelation.IsOrderConfirmed)
                    return false;
            }


            return true;
        }
    }
}
