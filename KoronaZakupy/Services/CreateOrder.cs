using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using System;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;
using KoronaZakupy.Services.Interfaces;

namespace KoronaZakupy.Services {
    public class CreateOrder : BaseOrderService, ICreateOrder
    {
        public CreateOrder(IOrdersRepository repo, IUnitOfWork unit) : base(repo, unit)
        {

        }

        public async Task<object> PlaceOrder(OrderModel model)
        {
            Order order = new Order {
                IsFinished = model.IsFinished,
                OrderDate = model.OrderDate
            };

            string _userId1 = model.UserId1;
            string _userId2 = model.UserId2;

            await _ordersRepository.CreateOrderAsync(order, _userId1, _userId2);
            await _unitOfWork.CompleteAsync();
           
            return("OK");
   
        }
    }
}
