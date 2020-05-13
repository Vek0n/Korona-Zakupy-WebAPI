using KoronaZakupy.Entities.OrdersDB;
using System;
using System.Collections.Generic;

namespace KoronaZakupy.Entities
{
    public class CompleteOrderDTO
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<string> Products { get; set; }
        
        public Order.OrderStatusEnum OrderStatus { get; set; }

        public List<UserDTO> UsersInfo { get; set; }

    }
}
