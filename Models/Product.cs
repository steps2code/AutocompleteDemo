using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCompleteAPIES.Models
{
    public class KeyWord
    {
        public List<string> input { get; set; }
    }

    public class Product
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int isPopularProduct { get; set; }
        public string category { get; set; }
        public List<KeyWord> keyWords { get; set; }
    }
}
