using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KoronaZakupy.Models {
    public class PlaceOrderModel {

        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string OrderType { get; set; }

        [Required]
        public List<string> Products { get; set; }

    }
}
