using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KoronaZakupy.Entities.UserDB
{
    public class Raiting
    {
        [Key]
        public int RaitingId { get; set; }

        public double Value { get; set; }

        public UserDb.User User { get; set; }

    }
}
