using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Repositories;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.UnitOfWork;

namespace KoronaZakupy.Services {

    public class UserRegister :  IUserRegister {

        private readonly ITokenGenerator _tokenGenerator;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserRegister
            (ITokenGenerator tokenGenerator,
            IOrdersRepository ordersRepository,
            IUnitOfWork unitOfWork) {
            _tokenGenerator = tokenGenerator;
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
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


            var u = (await userManager.FindByEmailAsync(model.Email)).Id;

            User userDB = new User {
                UserId = u
            };

            await _ordersRepository.CreateUserAsync(userDB);
            await _unitOfWork.CompleteAsync();

            //Sign in and generate token if register is succesful 
            if (result.Succeeded) {
                await signInManager.SignInAsync(user, false);
                return await _tokenGenerator.GenerateJwtToken(model.Email, user, configuration);
            }

            

            throw new ApplicationException("UNKNOWN_ERROR");
        }

    }
}
