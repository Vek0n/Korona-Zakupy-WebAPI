using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KoronaZakupy.Entities.UserDb {
    public class User : IdentityUser {
       
        public string Address { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }
          
        public string  PhotoDirectory { get; set; }

        [NotMapped]
        public string UserRole { get; set;}

        [NotMapped]
        public double Rating { get; set; }


    }
}
