using AutoMapper;
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
        protected readonly IMapper _mapper;

        public BaseOrderService(IOrdersRepository ordersRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
