using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class Order
    {
       [Key]
       public long OrderId { get; set; }
       public DateTime  OrderDate { get; set; }
       public bool IsFinished { get; set; }
       public bool user1Confirmed { get; set; }
       public bool user2Confirmed { get; set; }

        public ICollection<UserOrder> Users { get; set; }
    }
}
