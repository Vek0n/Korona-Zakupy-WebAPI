using KoronaZakupy.Entities;
using KoronaZakupy.Entities.OrdersDB;
using System;
using System.Collections.Generic;

namespace KoronaZakupy.Models
{
    public class OrderModel
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public Order.OrderTypeEnum OrderType { get; set; }
        public IEnumerable<string> Products { get; set; }
        public Order.OrderStatusEnum OrderStatus { get; set; }
        public List<UserDTO> UsersInfo { get; set; }
    }
}
