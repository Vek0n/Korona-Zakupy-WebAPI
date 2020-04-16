using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KoronaZakupy.Services.Interfaces;
using System.Linq;
using System;

namespace KoronaZakupy.Services {
    public class UsersGetter : IUserGetter{

        public IEnumerable<IdentityUser> GetUsers(UserManager<IdentityUser> userManager) {
            var users = userManager.Users.ToArray();
            return users;
        }

        public async Task<ActionResult<IdentityUser>> GetUser(string id, UserManager<IdentityUser> userManager) {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) {
                throw new ApplicationException("User Not Found");
            }
            return user;
        }
    }
}
