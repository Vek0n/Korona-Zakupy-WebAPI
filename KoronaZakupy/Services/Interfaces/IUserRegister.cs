using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;


namespace KoronaZakupy.Services.Interfaces {
    public interface IUserRegister {

        Task<object> Register(RegisterModel model,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration);
    }
}
