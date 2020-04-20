using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Services.Interfaces {
    public interface IUserRegister {

        Task<object> Register(RegisterModel validModel,
            UserManager<Entities.UserDb.User> userManager,
            SignInManager<Entities.UserDb.User> signInManager,
            IConfiguration configuration);
    }
}
