using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GithubSearchApp.Data.Models
{
    public class GH_ListResponse
    {
        [Key, JsonIgnore] 
        public int Id { get; set; }

        [JsonProperty("items")]
        public IEnumerable<GH_ItemResponse>? Items { get; set; }

        [JsonProperty("total_count")]
        public int total_count { get; set; }
    }

    [JsonObject("items")]
    public class GH_ItemResponse
    {
        [Key, JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int GH_ListResponseId { get; set; }

        [JsonIgnoreAttribute]
        public string? Search_text { get; set; }

        [JsonProperty("id")]    
        public string ItemId { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("stargazers_count")]
        public int Stargazers_count { get; set; }

        [JsonProperty("watchers_count")]
        public int Watchers_count { get; set; }

        [JsonProperty("html_url")]
        public string? Html_url { get; set; }

        public SimpleUser Owner { get; set; }
        public bool Private { get; set; }
    }

    [JsonObject("owner")]
    public class SimpleUser
    {
        [JsonIgnore] public int Id { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }
    }
}
