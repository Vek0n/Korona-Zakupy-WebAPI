using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class OrderDTO
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
     //   public Order.OrderTypeEnum OrderType { get; set; }
        public IEnumerable<string> Products { get; set; }
        public Order.OrderStatusEnum OrderStatus { get; set; }
        public List<string> UsersId { get; set; }
    }
}
