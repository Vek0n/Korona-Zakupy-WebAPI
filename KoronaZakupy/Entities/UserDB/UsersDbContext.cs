using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KoronaZakupy.Helpers;
using KoronaZakupy.Entities.UserDB;

namespace KoronaZakupy.Entities.UserDb {
    public class UsersDbContext : IdentityDbContext<Entities.UserDb.User>{

        public DbSet<Raiting> Raitings { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfiguration());

        }
    }
}

