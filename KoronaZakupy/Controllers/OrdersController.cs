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

        private readonly IOrdersRepository repo;
        private readonly IUnitOfWork unitOfWork;

        public OrdersController(
            ICreateOrder createOrder,
            IOrderGetter orderGetter,
            IUpdateOrder updateOrder,
            IOrdersRepository repo,
            IUnitOfWork unitOfWork) {
            _createOrder = createOrder;
            _orderGetter = orderGetter;
            _updateOrder = updateOrder;
            this.repo = repo;
            this.unitOfWork = unitOfWork;
        }

        ////// TODO: Tylko do testowania, na koniec usunąć
        //[AllowAnonymous]
        //[HttpGet("test")]
        //public async Task<IActionResult> Test()
        //{
            // await _updateOrder.ConfirmFinishedOrder(4, "26c3f897-04e2-4347-84c2-185d8be381cb");
            // await _updateOrder.ConfirmFinishedOrder(4, "4d701916-87aa-4b8c-9c76-b2935c52e87f");
            // var result = await _updateOrder.DidBothUsersConfirmedFinishedOrder(4);

            //var Order = new Order()
            //{
            //    OrderId = 5,
            //    OrderDate = new System.DateTime(2020, 4, 17),
            //    IsActive = false,
            //    IsFinished = true,
            //    Products = new string[]
            //    {
            //        "Harnas",
            //        "Tatra",
            //        "Zubr",
            //        "Cytryny"
            //    },
            //};

            //////User user = new User() { UserId = "26c3f897-04e2-4347-84c2-190sdadad07" };
            //await repo.UpdateAsync(Order);
            //await unitOfWork.CompleteAsync();

            //var xd = await repo.ChangeConfirmationOfOrderAsync(4, "4d701916-87aa-4b8c-9c76-b2935c52e87f");
            // await repo.UpdateAsync(xd);
            // await unitOfWork.CompleteAsync();

        //    var Order = new Order()
        //    {
        //        OrderId = 0,
        //        OrderDate = new System.DateTime(2020, 4, 17),
        //        IsActive = false,
        //        IsFinished = true,
        //        Products = new string[]
        //       {
        //            "Harnas",
        //            "Tatra",
        //            "Mydlo",
        //            "Cytryny"
        //       },
        //    };

        //    await repo.CreateAsync(Order, "4d701916-87aa-4b8c-9c76-b2935c52e87f");
        //    await unitOfWork.CompleteAsync();


        //    return Ok();
        //}

        [AllowAnonymous]
        [HttpGet("all/{id}")]
        public async Task<IEnumerable<OrderWithUsersInfo>> GetOrders(string id) {

            return await _orderGetter.GetOrdersAsync(id);          
        }


        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IEnumerable<OrderWithUsersInfo>> GetActiveOrders()
        {

            return  await _orderGetter.GetActiveOrdersAsync();
        }


        [AllowAnonymous]
        [HttpGet("active/{id}")]
        public async Task <IEnumerable<OrderWithUsersInfo>> GetUsersActiveOrders(string id) {

            return await _orderGetter.GetUserActiveOrdersAsync(id);
        }


        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<object> Add(OrderModel model) {

            return await _createOrder.PlaceOrder(model);
        }


        [AllowAnonymous]
        [HttpPost("accept/{id}/{userId}")]
        public async Task Accept(long id, string userId) {

             await _updateOrder.AcceptOrder(id, userId);
        }


        [AllowAnonymous]
        [HttpPost("accept/cancel/{id}/{userId}")]
        public async Task UnAccept(long id, string userId) {

            await _updateOrder.UnAcceptOrder(id, userId);
        }



        [AllowAnonymous]
        [HttpGet("finish/{id}")]
        public async Task CompleteOrder(long id) {

            await _updateOrder.FinishOrder(id);
            // TODO
            // IUpdateOrderResponsea
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
