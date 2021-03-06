﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Services.Interfaces {
    public interface IUserLogin {

        Task<object> Login(LoginModel model,
            UserManager<Entities.UserDb.User> userManager,
            SignInManager<Entities.UserDb.User> signInManager,
            IConfiguration configuration);
    }
}
