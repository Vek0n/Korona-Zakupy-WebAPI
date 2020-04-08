using System.Threading.Tasks;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.Communication.Interfaces;


namespace KoronaZakupy.Services.Interfaces {
    public interface ICreateOrder {

        Task<ICreateOrderResponse> PlaceOrder(Order newOrder);

    }
}
