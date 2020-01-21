using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGoods.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter first shipping address")]
        [Display(Name = "1st address")]
        public string Line1 { get; set; }

        [Display(Name = "2nd address")]
        public string Line2 { get; set; }

        [Display(Name = "3rd address")]
        public string Line3 { get; set; }

        [Required(ErrorMessage = "Enter shipping city")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required(ErrorMessage = "Enter shipping country")]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
