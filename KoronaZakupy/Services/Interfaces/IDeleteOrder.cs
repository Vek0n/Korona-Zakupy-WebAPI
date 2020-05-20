using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services.Interfaces
{
    public interface IDeleteOrder : IBaseOrder
    {
        Task Delete(long orderId);
    }
}
