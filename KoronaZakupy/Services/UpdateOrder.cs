using System;
using System.Threading.Tasks;
using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using AutoMapper;

namespace KoronaZakupy.Services {
    public class UpdateOrder : BaseOrderService, IUpdateOrder {

        UserManager<Entities.UserDb.User> _userManager;
        public UpdateOrder(IOrdersRepository repo, IUnitOfWork unit,
            UserManager<Entities.UserDb.User> userManager,
            IMapper mapper) : base(repo, unit, mapper)
        {
            _userManager = userManager;
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

            await ChangeActiveProperty(id);

            await _ordersRepository.AddRelationAsync(id, userId);
            await _unitOfWork.CompleteAsync();
        }


        public async Task UnAcceptOrder(long orderId, string userId) {

            await _ordersRepository.DeleteRelationAsync(orderId, userId);
            await _unitOfWork.CompleteAsync();

        }

        public async Task ChangeConfirmationOfFinishedOrder(long id, string userId)
        {
            var userOrder = await _ordersRepository.ChangeConfirmationOfOrderAsync(id, userId);
            await ChangeActiveProperty(id);
            await FinishUpdate(userOrder);
        }
    }
}
