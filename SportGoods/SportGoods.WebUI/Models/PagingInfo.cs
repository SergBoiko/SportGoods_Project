using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportGoods.WebUI.Models
{
    public class PagingInfo
    {
        public int TotalItems { get; set; }

        // Amount of product on one page
        public int ItemsPerPage { get; set; }

        // Number of current page
        public int CurrentPage { get; set; }

        // Total amount of pages
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
}