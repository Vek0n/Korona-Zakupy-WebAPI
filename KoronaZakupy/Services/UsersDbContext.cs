using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using KoronaZakupy.Entities;

namespace KoronaZakupy.Services {
    public class UsersDbContext : IdentityDbContext{

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

        }
    }
}

