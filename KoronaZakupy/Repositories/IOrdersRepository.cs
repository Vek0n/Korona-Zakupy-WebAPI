using System.Collections.Generic;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
namespace KoronaZakupy.Repositories
{
    public interface IOrdersRepository
    {

        Task CreateUserAsync(User user);

        #region Order CRUD
        Task CreateOrderAsync(Order order, string userId1, string userId2);
        Task UpdateOrderAsync(Order order);
        Task<Order> ReadOrderAsync(long id);
        Task<IEnumerable<Order>> ReadAllOrdersAsync();
        Task DeleteOrderAsync(long id);
        #endregion

        #region Other query
        Task<bool> DoesIdExist(long id);
        IEnumerable<OrderWithUsers> FindByUserId(string userId); //return -record id- with userId in it
        #endregion

    }
}