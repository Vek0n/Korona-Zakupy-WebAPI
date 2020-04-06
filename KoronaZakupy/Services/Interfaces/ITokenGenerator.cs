using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace KoronaZakupy.Services.Interfaces {
    public interface ITokenGenerator {

        Task<object> GenerateJwtToken(string email, IdentityUser user, IConfiguration configuration);

    }
}
