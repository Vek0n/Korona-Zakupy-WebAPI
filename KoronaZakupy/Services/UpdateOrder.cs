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
            var order = await _ordersRepository.FindOrderByOrderIdAsync(id);
            order.IsFinished = true;

            await FinishUpdate(order);
            
        }

        public async Task ChangeActiveProperty(long id)
        {
            var order = await _ordersRepository.FindOrderByOrderIdAsync(id);

            if (order.IsActive)
                order.IsActive = false;
            else
                order.IsActive = true;

            await FinishUpdate(order);
        }

      
        private async Task FinishUpdate<T>(T resource)
        {
            await _ordersRepository.UpdateAsync(resource);
            await _unitOfWork.CompleteAsync();
        }


        public async Task<bool> DidBothUsersConfirmedFinishedOrder(long id)
        {

            var order = await _ordersRepository.FindOrderByOrderIdAsync(id);
            
            foreach( var userOrderRelation in order.Users)
            {
                if (!userOrderRelation.IsOrderConfirmed)
                    return false;
            }

            return true;
        }

        public async Task AcceptOrder(long id, string userId) {

             await _ordersRepository.AddRelationAsync(id, userId);
             await _unitOfWork.CompleteAsync();
        }


        public async Task UnAcceptOrder(long id, string userId) {

            //REMEMBER: isActive = true;

        }

        public async Task ChangeConfirmationOfFinishedOrder(long id, string userId)
        {
            var userOrder = await _ordersRepository.ChangeConfirmationOfOrderAsync(id, userId);

            await FinishUpdate(userOrder);
        }
    }
}
