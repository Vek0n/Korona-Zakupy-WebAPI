using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services.Interfaces
{
    public interface IRatingService
    {
        Task AddRatingToUser(string userId, double value);
        Task<double> GetUserRating(string userId);
    }
}
