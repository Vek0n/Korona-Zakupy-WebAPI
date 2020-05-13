using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KoronaZakupy.Services.Interfaces;
using System.Linq;
using System;
using System.Reflection;

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


        public async Task<IList<string>> GetRole(string id, UserManager<Entities.UserDb.User> userManager) {
            var user = await userManager.FindByIdAsync(id);

            if (user == null) {
                throw new ApplicationException("User Not Found");
            }

            return (await userManager.GetRolesAsync(user));
        }

        public async Task<bool> IsExist(string resource, string name,UserManager<Entities.UserDb.User> userManager)
        {
            if(resource == "email")
            {
               var result = await userManager.FindByEmailAsync(name);
                
                if(result == null)
                    return false;

                return true;
            }
           
            if(resource == "name")
            {
                var result = await userManager.FindByNameAsync(name);
                if(result == null)
                {
                    return false;
                }
                return true;
            }

            else
            {
                throw new ApplicationException("This resource is not allowed!");
            }

        }
    }
}
