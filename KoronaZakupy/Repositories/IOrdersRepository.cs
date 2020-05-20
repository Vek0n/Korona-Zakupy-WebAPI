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

        void Delete<T>(T resource);

        Task<Order> GetOrderEntityAsync(long id);

        Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId, bool findByActivity=false);

        Task<IEnumerable<OrderDTO>> GetActiveOrdersAsync();

        Task<UserOrder> ChangeConfirmationOfOrderAsync(long orderId, string userId);
    }
}