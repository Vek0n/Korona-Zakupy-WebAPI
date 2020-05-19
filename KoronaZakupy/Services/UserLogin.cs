using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Entities;
using AutoMapper;

namespace KoronaZakupy.Services {
    public class UserLogin : IUserLogin{

        private readonly ITokenGenerator _tokenGenerator;
        public UserLogin(ITokenGenerator tokenGenerator, IMapper mapper) {
            _tokenGenerator = tokenGenerator;
        }

        public async Task<object> Login(LoginModel model,
            UserManager<Entities.UserDb.User> userManager,
            SignInManager<Entities.UserDb.User> signInManager,
            IConfiguration configuration) {

            var userName = await userManager.FindByEmailAsync(model.Email);
            var result = await signInManager.PasswordSignInAsync(userName.UserName, model.Password, false, false);

            if (result.Succeeded) {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                var token = await _tokenGenerator.GenerateJwtToken(model.Email, appUser, configuration);
                var userId = (await userManager.FindByEmailAsync(model.Email)).Id;
                var roleName = (await userManager.GetRolesAsync(appUser)).SingleOrDefault();
                var modelResponse = new LoginResponseModel
                {
                    UserId = userId,
                    Token = token,
                    RoleName = roleName
                };

                return modelResponse;

            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }


    }
}
