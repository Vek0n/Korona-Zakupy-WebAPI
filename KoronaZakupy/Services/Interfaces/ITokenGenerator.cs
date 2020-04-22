using System.Threading.Tasks;
using KoronaZakupy.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace KoronaZakupy.Services.Interfaces {
    public interface ITokenGenerator {

        Task<string> GenerateJwtToken(string email, Entities.UserDb.User user, IConfiguration configuration);

    }
}
