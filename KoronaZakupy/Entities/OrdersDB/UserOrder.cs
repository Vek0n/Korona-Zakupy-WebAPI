using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class UserOrder
    {

        public UserOrder(long id, string userId, bool isOrderConfirmed = false)
        {
            UserId = userId;
            OrderId = id;
            IsOrderConfirmed = isOrderConfirmed;
        }

        public string UserId { get; set; }
        public OrdersDB.User User { get; set; }

        public long OrderId { get; set; }
        public Order Order { get; set; }

        public bool IsOrderConfirmed { get; set; }

    }
}
