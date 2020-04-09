using System;
using System.ComponentModel.DataAnnotations;

namespace KoronaZakupy.Models {
    public class OrderModel {

        [Required]
        public string UserId1 { get; set; }

        [Required]
        public string UserId2 { get; set; }

        public DateTime OrderDate { get; set; }

        public bool IsFinished { get; set; }
    }
}
