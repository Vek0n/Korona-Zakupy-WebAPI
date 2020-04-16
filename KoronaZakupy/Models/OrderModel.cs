using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoronaZakupy.Models {
    public class OrderModel {

        [Required]
        public string UserId1 { get; set; }

        public DateTime OrderDate { get; set; }

        public List<string> Products { get; set; }

        public bool IsFinished { get; set; }

        public bool IsActive { get; set; }
    }
}
