using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Communication.Interfaces;
using KoronaZakupy.Models;
using System.Collections.Generic;


namespace KoronaZakupy.Services.Interfaces {
    public interface IOrderGetter : IBaseOrder {

        public Task<IEnumerable<OrderWithUsers>> GetOrdersAsync(string userId);
        public Task<IEnumerable<OrderWithUsers>> GetUserActiveOrdersAsync(string userId);

        public Task<IEnumerable<OrderWithUsers>> GetActiveOrdersAsync();
    }
}
