using AutoCompleteAPIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCompleteAPIES
{
    public class DBSearchHelper
    {
        private DatabaseContext _databaseContext;
        public DBSearchHelper(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public IEnumerable<Product> GetDBAutocompleteSuggestions(string term)
        {
            var matchProducts = from productEntity in _databaseContext.Products.ToList()
                                where productEntity.keyWords.ToLower().Contains(term.ToLower())
                                select new Product()
                                {
                                    id = productEntity.id.ToString(),
                                    name = productEntity.name
                                };


            return matchProducts;
        }

    }
}
