using System;
using System.Threading.Tasks;
using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
namespace KoronaZakupy.Services {
    public class UpdateOrder : BaseOrderService {
        public UpdateOrder(IOrdersRepository repo, IUnitOfWork unit) : base(repo, unit)
        {
            
        }
        private async Task Example()
        {
            //Use repo
            await _ordersRepository.ReadAllOrdersAsync();
            // If you create or update in database you must also use unitOfWork:
            await _unitOfWork.CompleteAsync();
        }
    }
}
