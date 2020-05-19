using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Repositories;
using KoronaZakupy.Entities.OrdersDB;
using KoronaZakupy.UnitOfWork;
using AutoMapper;
namespace KoronaZakupy.Services {

    public class UserRegister :  IUserRegister {

        private readonly ITokenGenerator _tokenGenerator;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRegister
            (ITokenGenerator tokenGenerator,
            IOrdersRepository ordersRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) {
            _tokenGenerator = tokenGenerator;
            _ordersRepository = ordersRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegisterResponseModel>Register(
            RegisterModel validModel,
            UserManager<Entities.UserDb.User> userManager,
            SignInManager<Entities.UserDb.User> signInManager,
            IConfiguration configuration) {


            var newUser = _mapper.Map<Entities.UserDb.User>(validModel);

            var result = await userManager.CreateAsync(newUser, validModel.Password);

            //Sign in and generate token if register is succesful 
            if (result.Succeeded) {
                var user = await userManager.FindByEmailAsync(validModel.Email);
       
                AddedToOrderDb(user.Id);

                await userManager.AddToRoleAsync(user, validModel.RoleName);
                await signInManager.SignInAsync(user, false);

                var token = await _tokenGenerator.GenerateJwtToken(validModel.Email, user, configuration);

                RegisterResponseModel model = new RegisterResponseModel {
                    UserId = user.Id,
                    Token = token
                };

                return model;
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private async void AddedToOrderDb(string userId)
        {
            User userDB = new User
            {
                UserId = userId
            };
            await _ordersRepository.CreateAsync(userDB);
            await _unitOfWork.CompleteAsync();
        }
    }
}
