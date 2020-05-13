using System.Threading.Tasks;

using KoronaZakupy.Models;

namespace KoronaZakupy.Services.Interfaces {
    public interface ICreateOrder : IBaseOrder
    {

        Task<object> PlaceOrder(PlaceOrderModel model);

    }
}
