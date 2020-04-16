using System.Collections.Generic;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
namespace KoronaZakupy.Repositories
{
    public interface IOrdersRepository
    {

        Task CreateUserAsync(User user);

        #region Order CRUD
        Task CreateOrderAsync(Order order, string userId);
        Task UpdateOrderAsync(Order order);
        Task UpdateUserOrderAsync(UserOrder userOrder);
        Task<Order> ReadOrderAsync(long id);
        Task<IEnumerable<Order>> ReadAllOrdersAsync();
        Task DeleteOrderAsync(long id);
        #endregion

        #region Other query
        Task<Order> FindByIdAsync(long id);
        Task AddRelationAsync(Order order,string userId);
        Task AddRelationAsync(long orderId, string userId);
        Task<bool> DoesIdExistAsync(long id);

        Task <IEnumerable<OrderWithUsers>> FindByUserIdAsync(string userId);
        Task<IEnumerable<OrderWithUsers>> FindActiveUserAsync();
        Task <IEnumerable<OrderWithUsers>> FindActiveOrdersByUserIdAsync(string userId);
        Task<UserOrder> ConfirmOrder(long orderId, string userId);
        Task<UserOrder> CancelOfConfirmationOrder(long orderId, string userId);
        #endregion

    }
}