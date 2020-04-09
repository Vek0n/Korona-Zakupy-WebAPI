using KoronaZakupy.Repositories;
using KoronaZakupy.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace KoronaZakupy.Services {
    public class CreateOrder : BaseOrderService
    {
        public CreateOrder(IOrdersRepository repo, IUnitOfWork unit) : base(repo, unit)
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
