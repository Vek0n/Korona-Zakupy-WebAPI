﻿using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Communication.Interfaces;


namespace KoronaZakupy.Services.Interfaces {
    public interface IUpdateOrder : IBaseOrder
    {

        //Task<IUpdateOrderResponse> FinishOrder(Order updatedOrder, string id);
        Task ChangeActiveProperty(long id);
        Task FinishOrder(long id);
        Task<bool> DidBothUsersConfirmedFinishedOrder(long id);
        Task ChangeConfirmationOfFinishedOrder(long id, string userId);
        Task AcceptOrder(long id, string userId);
        Task UnAcceptOrder(long id, string userId);
    }
}
