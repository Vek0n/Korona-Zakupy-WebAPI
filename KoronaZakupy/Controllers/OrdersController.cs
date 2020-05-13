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
        private readonly IMapper _mapper;

        private readonly IOrdersRepository repo;
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(
            ICreateOrder createOrder,
            IOrderGetter orderGetter,
            IUpdateOrder updateOrder,
            IMapper mapper,
            IOrdersRepository repo,
            IUnitOfWork unitOfWork) {
            _createOrder = createOrder;
            _orderGetter = orderGetter;
            _updateOrder = updateOrder;
            _mapper = mapper;
            this.repo = repo;
            this.unitOfWork = unitOfWork;
        }

        [AllowAnonymous]
        [HttpGet("all/{id}")]
        public async Task<IEnumerable<OrderModel>> GetOrders(string id) {

            return  _mapper.Map<IEnumerable<OrderModel>>( await _orderGetter.GetOrdersAsync(id) );          
        }


        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IEnumerable<OrderModel>> GetActiveOrders()
        {
            return _mapper.Map<IEnumerable<OrderModel>>(await _orderGetter.GetActiveOrdersAsync() );
        }


        [AllowAnonymous]
        [HttpGet("active/{id}")]
        public async Task <IEnumerable<OrderModel>> GetUsersActiveOrders(string id) {

            return _mapper.Map<IEnumerable<OrderModel>>(await _orderGetter.GetUserActiveOrdersAsync(id));
        }


        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<object> Add( [FromBody] PlaceOrderModel model) {

            return await _createOrder.PlaceOrder(model);
        }


        [AllowAnonymous]
        [HttpPost("accept/{id}/{userId}")]
        public async Task Accept(long id, string userId) {

             await _updateOrder.AcceptOrder(id, userId);
        }


        [AllowAnonymous]
        [HttpPost("accept/cancel/{id}/{userId}")]
        public async Task UnAccept(long id, string userId){

            await _updateOrder.UnAcceptOrder(id, userId);
        }



        [AllowAnonymous]
        [HttpGet("finish/{id}")]
        public async Task CompleteOrder(long id) {

            await _updateOrder.FinishOrder(id);   
        }

        [AllowAnonymous]
        [HttpGet("confirm/{id}/{userId}")]
        public async Task ConfirmFinishedOrder(long id, string userId) {

           await _updateOrder.ChangeConfirmationOfFinishedOrder(id, userId);

        }


        [AllowAnonymous]
        [HttpGet("confirm/cancel/{id}/{userId}")]
        public async Task CancelConfirmation(long id, string userId) {

            await _updateOrder.ChangeConfirmationOfFinishedOrder(id,userId);

        }

        [AllowAnonymous]
        [HttpGet("confirm/check/{id}")]
        public async Task<bool> CheckConfirmation(long id) {

            return await _updateOrder.DidBothUsersConfirmedFinishedOrder(id);

        }


    }
}
