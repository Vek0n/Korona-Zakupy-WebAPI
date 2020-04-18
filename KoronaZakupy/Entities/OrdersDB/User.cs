using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoronaZakupy.Entities.OrdersDB
{
    public class User 
    {
        [Key]
        public string UserId { get; set; }

        public ICollection<UserOrder> Orders { get; set; }
    }
}
