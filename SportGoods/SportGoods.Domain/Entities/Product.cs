using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SportGoods.Domain.Entities
{
    public class Product
    {
        public Product()
        {
            Orders = new List<Order>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter a name of product, please")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Enter a description of product, please")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Choose a category for product, please")]
        public string Category { get; set; }

        [Display(Name = "Price ($)")]
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Enter a positive value for price, please")]
        public decimal Price { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
