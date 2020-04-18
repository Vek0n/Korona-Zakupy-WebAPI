using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Communication.Interfaces;
using KoronaZakupy.Models;
using System.Collections.Generic;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Services.Interfaces {
    public interface IOrderGetter : IBaseOrder {

        public Task<IEnumerable<OrderWithUsersInfo>> GetOrdersAsync(string userId);
        public Task<IEnumerable<OrderWithUsersInfo>> GetUserActiveOrdersAsync(string userId);

        public Task<IEnumerable<OrderWithUsersInfo>> GetActiveOrdersAsync();
    }
}
