using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KoronaZakupy.Entities.UserDb {
    public class User : IdentityUser {
       
        public string Address { get; set; }
       
        public string FirstName { get; set; }

        public string LastName { get; set; }
      
        public decimal Rating { get; set; }
        
        public string  PhotoDirectory { get; set; }

    }
}
