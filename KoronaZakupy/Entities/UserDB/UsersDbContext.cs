using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KoronaZakupy.Helpers;

namespace KoronaZakupy.Entities.UserDb {
    public class UsersDbContext : IdentityDbContext<Entities.UserDb.User>{

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());

        }
    }
}

