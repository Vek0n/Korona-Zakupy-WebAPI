using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KoronaZakupy.Services.Interfaces;
using System.Linq;
using System;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Services {
    public class UsersGetter : IUserGetter{

        public IEnumerable<Entities.UserDb.User> GetUsers(UserManager<Entities.UserDb.User> userManager) {
            var users = userManager.Users.ToArray();
            return users;
        }

        public async Task<ActionResult<Entities.UserDb.User>> GetUser(string id, UserManager<Entities.UserDb.User> userManager) {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) {
                throw new ApplicationException("User Not Found");
            }
            return user;
        }
    }
}
