using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KoronaZakupy.Helpers
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Name = "Volunteer",
                NormalizedName = "VOLUNTEER"
            },

             new IdentityRole
             {
                 Name = "PersonInQuarantine",
                 NormalizedName = "PERSONINQUARANTINE"
             });
         
        }
    }
}
