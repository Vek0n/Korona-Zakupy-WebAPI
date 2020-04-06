using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace KoronaZakupy.Services.Interfaces {
    public interface IUserGetter {

        public IEnumerable<IdentityUser> GetUsers(UserManager<IdentityUser> userManager);

        Task<ActionResult<IdentityUser>> GetUser(string id, UserManager<IdentityUser> userManager);

    }
}
