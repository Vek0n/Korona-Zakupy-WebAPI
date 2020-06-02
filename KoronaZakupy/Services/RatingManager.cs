using KoronaZakupy.Entities.UserDb;
using KoronaZakupy.Entities.UserDB;
using KoronaZakupy.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Services
{
    public class RatingManager : IRatingManager
    {
        private UsersDbContext _usersDb;
        private UserManager<Entities.UserDb.User> _userManager;

        public RatingManager(UsersDbContext usersDb, UserManager<Entities.UserDb.User> userManager)
        {
            _usersDb = usersDb;
            _userManager = userManager;
        }

        public async Task AddNewRatingToUser(double value, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var newRating = new Raiting
            {
                RaitingId = 0,
                Value = value,
                User = user
            };

            await _usersDb.AddAsync(newRating);
            await _usersDb.SaveChangesAsync();

        }

        public async Task<double> GetAverageRating(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = 0.0d;
            if (await _usersDb.Raitings.AsNoTracking().FirstOrDefaultAsync(rating => rating.User == user) != null)
            {
               result = _usersDb.Raitings.AsNoTracking().Where(rating => rating.User == user).Select(rating => rating.Value).Average();
            }

            return result;
        }

    }
}
