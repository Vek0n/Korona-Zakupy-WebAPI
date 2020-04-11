using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoronaZakupy.Models;
using Microsoft.AspNetCore.Authorization;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Entities.OrdersDB;


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

        [AllowAnonymous]
        [HttpGet("confirm/{id}")]
        public async Task ConfirmFinishedOrder(long id) {

            await _updateOrder.ConfirmFinishedOrder(id);

        }


        [AllowAnonymous]
        [HttpGet("confirm/cancel/{id}")]
        public async Task CancelConfirmation(long id) {

            await _updateOrder.CancelConfirmationOfFinisedOrder(id);

        }

        [AllowAnonymous]
        [HttpGet("confirm/check")]
        public async Task<bool> CheckConfirmation(long id) {

            return await _updateOrder.DidBothUsersConfirmedFinishedOrder(id);

        }




    }
}
