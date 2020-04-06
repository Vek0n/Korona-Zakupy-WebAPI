using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Services.Interfaces;

namespace KoronaZakupy.Services {

    public class UserRegister : IUserRegister  {

        private readonly ITokenGenerator _tokenGenerator;

        public UserRegister(ITokenGenerator tokenGenerator) {
            _tokenGenerator = tokenGenerator;
        }


        public async Task<object>Register(
            RegisterModel model,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration) {

            // Copy data from RegisterModel to IdentityUser
            var user = new IdentityUser {
                UserName = model.Email,
                Email = model.Email
            };

            // Store user data in database table
            var result = await userManager.CreateAsync(user, model.Password);

            //Sign in and generate token if register is succesful 
            if (result.Succeeded) {
                await signInManager.SignInAsync(user, false);
                return await _tokenGenerator.GenerateJwtToken(model.Email, user, configuration);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

    }
}
