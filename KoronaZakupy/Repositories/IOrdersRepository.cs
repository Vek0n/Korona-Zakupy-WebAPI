using System.Threading.Tasks;
using KoronaZakupy.Entities;
namespace KoronaZakupy.Repositories
{
    public interface IOrdersRepository
    {


        Task<bool> DoesIdExist(string id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task<string> FindByUserId(string userId); //return -record id- with userId in it
        

    }
}