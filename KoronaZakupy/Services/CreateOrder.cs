using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using System;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;
using KoronaZakupy.Services.Interfaces;
using AutoMapper;

namespace KoronaZakupy.Services {
    public class CreateOrder : BaseOrderService, ICreateOrder
    {
        public CreateOrder(IOrdersRepository repo, IUnitOfWork unit, IMapper mapper) : base(repo, unit,mapper)
        {

        }

        public async Task<object> PlaceOrder(PlaceOrderModel model)
        {
            await _ordersRepository.CreateAsync(_mapper.Map<Order>(model), model.UserId);
            await _unitOfWork.CompleteAsync();
          
            return("OK");
   
        }
    }
}
