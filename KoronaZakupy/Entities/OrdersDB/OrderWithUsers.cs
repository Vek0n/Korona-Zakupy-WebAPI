using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class OrderWithUsers
    {
        public long OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public bool IsFinished { get; set; }
        //public bool user1Confirmed { get; set; }
        //public bool user2Confirmed { get; set; }

        public List<string> UsersId { get; set; }
    }
}
