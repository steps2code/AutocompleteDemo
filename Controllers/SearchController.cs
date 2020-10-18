using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoCompleteAPIES.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AutoCompleteAPIES.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ESSearchHelper _esSearchHelper;
        private DBSearchHelper _dBSearchHelper;

        public SearchController(ESSearchHelper esSearchHelper, DBSearchHelper dBSearchHelper)
        {
            _esSearchHelper = esSearchHelper;
            _dBSearchHelper = dBSearchHelper;
        }

        [HttpGet]
        [ActionName("GetAutocompleteSuggestions")]
        public IActionResult GetAutocompleteSuggestions(string term)
        {
            var suggestedProducts = _esSearchHelper.GetAutocompleteSuggestions(term);
            if (suggestedProducts != null && suggestedProducts.ToList().Count > 0)
                return Ok(suggestedProducts);
            else
                return NotFound();
        }

        [HttpGet]
        [ActionName("GetDBAutocompleteSuggestions")]
        public IActionResult GetAutocpmpleteData(string term)
        {
            if (!string.IsNullOrEmpty(term))
            {
                var suggestedProducts = _dBSearchHelper.GetDBAutocompleteSuggestions(term);
                if (suggestedProducts != null && suggestedProducts.ToList().Count > 0)
                    return Ok(suggestedProducts);
                else
                    return NotFound();
            }
            else
                return BadRequest();
        }
    }
}