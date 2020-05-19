using KoronaZakupy.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services
{
    public class RatingService : IRatingService
    {
        private readonly IRatingManager _ratingManager;

       public RatingService(IRatingManager ratingManager)
        {
            _ratingManager = ratingManager;
        }

        public async Task AddRatingToUser(string userId, double value)
        {
            await _ratingManager.AddNewRatingToUser(value, userId);
        }

        public async Task<double> GetUserRating(string userId)
        {
            return await _ratingManager.GetAverageRating(userId);
        }
    }
}
