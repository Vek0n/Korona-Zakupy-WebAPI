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
            // TODO: ADD MAPPING
            Order order = new Order {

                OrderId = 0,
                OrderDate = model.OrderDate,
                Products = model.Products,
                IsActive = true,
                IsFinished = false

            };

            await _ordersRepository.CreateAsync(order,model.UserId1);
            await _unitOfWork.CompleteAsync();
           
            return("OK");
   
        }
    }
}
