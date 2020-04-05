using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using KoronaZakupy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KoronaZakupy.Controllers {

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase {

        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;

        public UserController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration) {

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
        }


        [HttpGet]
        public IEnumerable<IdentityUser> Get() {
            var users = userManager.Users.ToArray();
            return users;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<IdentityUser>> GetUser(string id) {
            var user = await userManager.FindByIdAsync(id);
            if (user == null) {
                return NotFound();
            }
            return user;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<object> Register([FromBody] RegisterModel model) {

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
                return await GenerateJwtToken(model.Email, user);
            }


            foreach (var error in result.Errors) {
                ModelState.AddModelError(string.Empty, error.Description);
            }         

            throw new ApplicationException("UNKNOWN_ERROR");
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<object> Login([FromBody] LoginModel model) {

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                
            if (result.Succeeded) {
                var appUser = userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                return await GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT"); 
        }


        
        private async Task<object> GenerateJwtToken(string email, IdentityUser user) {

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpireDays"]));

            var token = new JwtSecurityToken(
                configuration["JwtIssuer"],
                configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
