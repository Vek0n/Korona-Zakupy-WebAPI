﻿using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Communication.Interfaces;


namespace KoronaZakupy.Services.Interfaces {
    public interface IUpdateOrder : IBaseOrder
    {

        Task<IUpdateOrderResponse> UpdateOrder(Order updatedOrder, string id);

    }
}
