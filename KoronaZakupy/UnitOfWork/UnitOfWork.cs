using KoronaZakupy.Entities.OrdersDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrdersDbContext _orderDb;

        public UnitOfWork(OrdersDbContext orderDb)
        {
            _orderDb = orderDb;
        }

        public async Task CompleteAsync()
        {
            await _orderDb.SaveChangesAsync();
        }
    }
}
