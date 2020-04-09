﻿using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using System;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;
using KoronaZakupy.Services.Interfaces;
using System.Collections.Generic;

namespace KoronaZakupy.Services {
    public class OrderGetter : BaseOrderService, IOrderGetter {

        public OrderGetter(IOrdersRepository repo, IUnitOfWork unit) : base(repo, unit) {

        }

       public IEnumerable<Order> GetOrders(string userId) {

            return _ordersRepository.FindByUserId(userId);
            
        }

    }
}
