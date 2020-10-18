using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCompleteAPIES.Models.EntityModel
{
    public class ProductEntity
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double price { get; set; }
        public bool isPopularProduct { get; set; }
        public string category { get; set; }
        public string keyWords { get; set; }
    }
}
