using KoronaZakupy.Entities;
using KoronaZakupy.Entities.OrdersDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services.Interfaces
{
   public interface ICompleteUserInfo
    {
        public Task<IEnumerable<OrderWithUsersInfo>> CompleteAsync(IEnumerable<OrderWithUsersId> order);
    }
}
