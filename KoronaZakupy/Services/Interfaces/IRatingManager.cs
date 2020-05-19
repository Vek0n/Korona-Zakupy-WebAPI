using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services.Interfaces
{
   public interface IRatingManager
    {
        Task AddNewRatingToUser(double value, string userId);
        Task<double> GetAverageRating(string userId);
    }
}
