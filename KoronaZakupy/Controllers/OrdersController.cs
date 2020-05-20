using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoronaZakupy.Models;
using Microsoft.AspNetCore.Authorization;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using KoronaZakupy.Entities;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Identity;

namespace KoronaZakupy.Controllers {

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {

        private readonly ICreateOrder _createOrder;
        private readonly IOrderGetter _orderGetter;
        private readonly IUpdateOrder _updateOrder;
        private readonly IDeleteOrder _deleteOrder;
        private readonly IMapper _mapper;

        public OrdersController(
            ICreateOrder createOrder,
            IOrderGetter orderGetter,
            IUpdateOrder updateOrder,
            IMapper mapper,
            IDeleteOrder deleteOrder) {
            _createOrder = createOrder;
            _orderGetter = orderGetter;
            _updateOrder = updateOrder;
            _deleteOrder = deleteOrder;
            _mapper = mapper;
        }

        [HttpGet("all/{id}")]
        public async Task<IEnumerable<OrderModel>> GetOrders(string id) {

            return  _mapper.Map<IEnumerable<OrderModel>>( await _orderGetter.GetOrdersAsync(id) );          
        }

        [HttpGet("active")]
        public async Task<IEnumerable<OrderModel>> GetActiveOrders()
        {
            return _mapper.Map<IEnumerable<OrderModel>>(await _orderGetter.GetActiveOrdersAsync() );
        }

        [HttpGet("active/{id}")]
        public async Task <IEnumerable<OrderModel>> GetUsersActiveOrders(string id) {

            return _mapper.Map<IEnumerable<OrderModel>>(await _orderGetter.GetUserActiveOrdersAsync(id));
        }

        [HttpPost("add")]
        public async Task<object> PlaceOrder( [FromBody] PlaceOrderModel model) {

            return await _createOrder.PlaceOrder(model);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(long orderId)
        {
            await _deleteOrder.Delete(orderId);

            return Ok();
        }

        [HttpPost("accept/{id}/{userId}")]
        public async Task Accept(long id, string userId) {

             await _updateOrder.AcceptOrder(id, userId);
        }

        [HttpPost("accept/cancel/{id}/{userId}")]
        public async Task UnAccept(long id, string userId){

            await _updateOrder.UnAcceptOrder(id, userId);
        }

        [HttpPost("confirm/{id}/{userId}")]
        public async Task ConfirmFinishedOrder(long id, string userId) {

           await _updateOrder.ConfirmAndFinishOrder(id, userId);
        }

        [HttpPost("confirm/cancel/{id}/{userId}")]
        public async Task CancelConfirmation(long id, string userId) {

            await _updateOrder.ConfirmAndFinishOrder(id,userId);
        }
    }
}
