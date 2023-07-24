using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace GithubSearchApp.Data.Models
{
    public class SearchResult
    {
        [Key, JsonIgnore]
        public int SearchResult_id { get; set; }

        public string? SearchText { get; set; }
        public string? JsonContent { get; set; }
    }
}
