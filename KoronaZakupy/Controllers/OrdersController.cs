using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Repositories;
using KoronaZakupy.Entities.OrdersDB;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace KoronaZakupy.Controllers {

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController {

        private readonly ICreateOrder _createOrder;
        private readonly IOrderGetter _orderGetter;
        private readonly IUpdateOrder _updateOrder;

        public OrdersController(
            ICreateOrder createOrder,
            IOrderGetter orderGetter,
            IUpdateOrder updateOrder) {
            _createOrder = createOrder;
            _orderGetter = orderGetter;
            _updateOrder = updateOrder;
           
        }

        [AllowAnonymous]
        [HttpGet("all/{id}")]
        public IEnumerable<OrderWithUsers> GetOrders(string id) {

            return _orderGetter.GetOrders(id);
                    
        }


        [AllowAnonymous]
        [HttpGet("active/{id}")]
        public IEnumerable<OrderWithUsers> GetActiveOrders(string id) {

            return _orderGetter.GetActiveOrders(id);

        }


        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<object> Add(OrderModel model) {

            return await _createOrder.PlaceOrder(model);
        }


        [AllowAnonymous]
        [HttpGet("finish/{id}")]
        public async Task CompleteOrder(long id) {

            await _updateOrder.FinishOrder(id);
            // TODO
            // IUpdateOrderResponse
        }

    }
}
