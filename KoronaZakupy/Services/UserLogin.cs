using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Services.Interfaces;


namespace KoronaZakupy.Services {
    public class UserLogin : IUserLogin{

        private readonly ITokenGenerator _tokenGenerator;

        public UserLogin(ITokenGenerator tokenGenerator) {
            _tokenGenerator = tokenGenerator;
        }

        public async Task<object> Login(LoginModel model,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration) {

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

            if (result.Succeeded) {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return await _tokenGenerator.GenerateJwtToken(model.Email, appUser, configuration);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }


    }
}
