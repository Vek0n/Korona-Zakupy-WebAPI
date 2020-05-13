using System.Threading.Tasks;

namespace KoronaZakupy.Services.Interfaces {
    public interface IUpdateOrder : IBaseOrder
    {
        Task ConfirmAndFinishOrder(long id, string userId);
        Task AcceptOrder(long id, string userId);
        Task UnAcceptOrder(long id, string userId);
    }
}
