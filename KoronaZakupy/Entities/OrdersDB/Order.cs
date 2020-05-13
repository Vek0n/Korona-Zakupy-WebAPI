﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class Order 
    {
        public enum OrderStatusEnum
        {
            Avalible,
            InProgress,
            AwaitingConfirmation,
            Finished
        }

        [Key]
        public long OrderId { get; set; }

        public DateTime  OrderDate { get; set; }

       
        public IEnumerable<string> Products { get; set; }

        public OrderStatusEnum OrderStatus { get; set; }

       // public bool IsFinished { get; set; }

       //public bool IsActive { get; set; }

        public ICollection<UserOrder> Users { get; set; }
    }
}
