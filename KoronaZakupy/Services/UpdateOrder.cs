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
       
        public async Task ConfirmAndFinishOrder(long orderId, string userId)
        {
            await ChangeIsConfirmedInUserOrder(orderId, userId);
            await ChangeConfirmationOfOrder(orderId, userId);
        }

        private async Task ChangeIsConfirmedInUserOrder(long orderId, string userId)
        {
            var userOrder = await _ordersRepository.ChangeConfirmationOfOrderAsync(orderId, userId);
            await FinishUpdate(userOrder);
        }
        
        private async Task ChangeConfirmationOfOrder(long orderId, string userId)
        {
            var order = await _ordersRepository.FindOrderByOrderIdAsync(orderId);
            var counter = 0;

            foreach (var userOrderRelation in order.Users)
            {
                if (userOrderRelation.IsOrderConfirmed)
                    counter++;
            }

            if (counter == 1)
                await ChangeOrderStatus(orderId, Order.OrderStatusEnum.AwaitingConfirmation);
            else if (counter == 2)
                await ChangeOrderStatus(orderId, Order.OrderStatusEnum.Finished);
            else
                throw new ApplicationException();
        }

        public async Task AcceptOrder(long id, string userId) {

            await ChangeOrderStatus(id,Order.OrderStatusEnum.InProgress);

            await _ordersRepository.AddRelationAsync(id, userId);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UnAcceptOrder(long orderId, string userId) {

            await ChangeOrderStatus(orderId,Order.OrderStatusEnum.Avalible);

            await _ordersRepository.DeleteRelationAsync(orderId, userId);
            await _unitOfWork.CompleteAsync();

        }

        private async Task ChangeOrderStatus(long id, Order.OrderStatusEnum newStatus)
        {
            var order = await _ordersRepository.FindOrderByOrderIdAsync(id);

            order.OrderStatus = newStatus;

            await FinishUpdate(order);
        }

        private async Task FinishUpdate<T>(T resource)
        {
            await _ordersRepository.UpdateAsync(resource);
            await _unitOfWork.CompleteAsync();
        }
    }
}
