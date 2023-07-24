using GithubSearchApp.Data;
using GithubSearchApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace GithubSearchApp.API
{
    public class SearchRepository : IRepository<SearchResult>
    {
        private readonly AppDbContext _context;
        public SearchRepository(AppDbContext context) => _context = context;

        public async Task<bool> Create(SearchResult entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(SearchResult entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SearchResult> Get(string searchText)
        {
            return await _context.SearchResults.FirstOrDefaultAsync(x => x.SearchText == searchText);
        }

        public async Task<List<SearchResult>> GetAll()
        {
            return await _context.SearchResults.ToListAsync();
        }

        //public async Task<SearchResult> Update(SearchResult entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
