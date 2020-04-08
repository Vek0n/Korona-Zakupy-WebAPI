using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.UnitOfWork
{
    interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
