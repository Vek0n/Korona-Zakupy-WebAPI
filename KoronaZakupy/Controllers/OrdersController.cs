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

        public OrdersController(
            ICreateOrder createOrder,
            IOrderGetter orderGetter) {
            _createOrder = createOrder;
            _orderGetter = orderGetter;
           
        }


        [AllowAnonymous]
        [HttpGet("{id}")]
        public IEnumerable<OrderWithUsers> GetOrders(string id) {

            var result = _orderGetter.GetOrders(id);

            return result;
        }


        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<object> Add(OrderModel model) {

            return await _createOrder.PlaceOrder(model);

        }

        



    }
}
