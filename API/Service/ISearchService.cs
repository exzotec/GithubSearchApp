using GithubSearchApp.Data.Models;

namespace GithubSearchApp.API
{
    public interface ISearchService
    {
        Task<bool> Search(string searchText);

        Task<bool> Delete(string id);

        Task<IEnumerable<SearchResult>> GetAll();   
    }
}
