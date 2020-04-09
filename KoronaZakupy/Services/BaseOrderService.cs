using KoronaZakupy.Repositories;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services
{
    public abstract class BaseOrderService : IBaseOrder
    {
        protected readonly IOrdersRepository _ordersRepository;
        protected readonly IUnitOfWork _unitOfWork;

        public BaseOrderService(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
        }
    }
}
