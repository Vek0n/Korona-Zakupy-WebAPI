using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;


namespace KoronaZakupy.Services.Interfaces {
    public interface IUserLogin {

        Task<object> Login(LoginModel model,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration);
    }
}
