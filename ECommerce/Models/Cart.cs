using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public Product CartProducts { get; set; }

        public int NumberOfProducts { get; set; }
    }
}