using System.Collections.Generic;
using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
namespace KoronaZakupy.Repositories
{
    public interface IOrdersRepository
    {
      
        Task CreateAsync<T>(T resource, string userId = "");

        Task AddRelationAsync(long orderId, string userId);

        Task DeleteRelationAsync(long orderId, string userId);

        Task UpdateAsync<T>(T resource);

        Task<Order> FindOrderByOrderIdAsync(long id);

        Task<IEnumerable<OrderDTO>> FindOrdersByUserIdAsync(string userId, bool findByActivity=false);

        Task<IEnumerable<OrderDTO>> FindActiveOrdersAsync();

        Task<UserOrder> ChangeConfirmationOfOrderAsync(long orderId, string userId);
    }
}