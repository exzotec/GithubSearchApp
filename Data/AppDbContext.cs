using GithubSearchApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GithubSearchApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<SearchResult> SearchResults { get; set; }
        public DbSet<GH_ListResponse> GH_ListResponses { get; set; }
        public DbSet<GH_ItemResponse> GH_ItemResponses { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<SearchResult>().HasNoKey();
            
        }
    }
}
