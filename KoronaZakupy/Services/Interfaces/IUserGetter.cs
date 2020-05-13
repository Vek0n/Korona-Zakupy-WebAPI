using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Services.Interfaces {
    public interface IUserGetter {

        public IEnumerable<Entities.UserDb.User> GetUsers(UserManager<Entities.UserDb.User> userManager);

        Task<ActionResult<Entities.UserDb.User>> GetUser(string id, UserManager<Entities.UserDb.User> userManager);
        Task<IList<string>> GetRole(string id,UserManager<Entities.UserDb.User> userManager);
        Task<bool> IsExist(string resource, string name, UserManager<Entities.UserDb.User> userManager);
    }
}
