using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using KoronaZakupy.Services.Interfaces;
using KoronaZakupy.Repositories;
using KoronaZakupy.Entities.OrdersDB;
using System.Linq;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoronaZakupy.Controllers {

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IUserRegister _userRegister;
        private readonly IUserLogin _userLogin;
        private readonly IUserGetter _userGetter;
 
        public UserController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            IUserRegister userRegister,
            IUserLogin userLogin,
            IUserGetter userGetter) {

            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _userRegister = userRegister;
            _userLogin = userLogin;
            _userGetter = userGetter;
        }

        [HttpGet]
        public IEnumerable<IdentityUser> Get() {

            return _userGetter.GetUsers(_userManager);

        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityUser>> GetUser(string id) {

            return await _userGetter.GetUser(id, _userManager);

        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterModel model) {

            return await _userRegister.Register(model, _userManager, _signInManager, _configuration);

        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginModel model) {

            return await _userLogin.Login(model, _userManager, _signInManager, _configuration);
        }
    }
}
