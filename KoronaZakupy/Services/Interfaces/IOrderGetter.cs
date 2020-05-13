using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Models;
using System.Collections.Generic;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Services.Interfaces {
    public interface IOrderGetter : IBaseOrder {

        public Task<IEnumerable<CompleteOrderDTO>> GetOrdersAsync(string userId);
        public Task<IEnumerable<CompleteOrderDTO>> GetUserActiveOrdersAsync(string userId);

        public Task<IEnumerable<CompleteOrderDTO>> GetActiveOrdersAsync();
    }
}
