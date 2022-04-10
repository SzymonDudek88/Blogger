using Cosmonaut.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Cosmos
{
    [CosmosCollection("posts")]
   public class CosmosPost
    {
        [CosmosPartitionKey] // po wgraniu tego mamy konflikty i dlatego instalujemy do web api humanizer.core
        [JsonProperty] // wymagane
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
