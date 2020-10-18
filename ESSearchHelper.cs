using AutoCompleteAPIES.Models;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoCompleteAPIES
{
    public class ESSearchHelper
    {
        private IConfiguration _iConfig;
        string INDEX_NAME = "productcatalog";
        public ESSearchHelper(IConfiguration iConfig)
        {
            _iConfig = iConfig;
        }

        public ElasticClient GetESClient()
        {
            string connectionString = _iConfig.GetSection("ConnectionStrings").GetSection("EsConnection").Value;
            ConnectionSettings connectionSettings;
            ElasticClient elasticClient;
            StaticConnectionPool connectionPool;
            //Multiple node for fail over (cluster addresses)
            var nodes = new Uri[] { new Uri(connectionString) };

            connectionPool = new StaticConnectionPool(nodes);
            connectionSettings = new ConnectionSettings(connectionPool);
            elasticClient = new ElasticClient(connectionSettings);
            return elasticClient;
        }

        public  IEnumerable<Product> GetAutocompleteSuggestions( string keyword)
        {
            ElasticClient elasticClient = GetESClient();
            ISearchResponse<Product> searchResponse = elasticClient.Search<Product>(s => s
                                     .Index(INDEX_NAME)
                                     .Suggest(su => su
                                          .Completion("suggestedProducts", c => c
                                               .Field(f => f.keyWords)
                                               .Prefix(keyword)
                                               .Fuzzy(f => f
                                                   .Fuzziness(Fuzziness.Auto)
                                               )
                                               .Size(5))
                                             ));

            var suggests = from suggest in searchResponse.Suggest["suggestedProducts"]
                           from option in suggest.Options
                           select new Product
                           {
                               id = option.Source.id,
                               name = option.Source.name,
                               description = option.Source.description
                           };


            return suggests;
        }
    }
}
