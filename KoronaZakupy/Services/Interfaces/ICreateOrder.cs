using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Communication.Interfaces;
using KoronaZakupy.Models;

namespace KoronaZakupy.Services.Interfaces {
    public interface ICreateOrder : IBaseOrder
    {

        Task<object> PlaceOrder(OrderModel model);

    }
}
