//using GithubSearchApp.API;
//using GithubSearchApp.Data;
//using GithubSearchApp.Data.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Threading.Tasks;

//namespace GithubSearchApp.Api
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class SearchController : ControllerBase
//    {
//        private readonly ISearchService _service;

//        public SearchController(ISearchService service) => _service = dbContextervice;

//        [HttpPost]
//        public async Task<IActionResult> Search([FromBody] string searchText)
//        {
//            await _service.Search(searchText);
//            return Ok();
//        }

//        //// Если результаты поиска не найдены, отправляем запрос на API GitHub
//        //var httpClient = _httpClientFactory.CreateClient();
//        //var apiUrl = $"https://api.github.com/search/repositories?q={SearchText}";
//        //var response = await httpClient.GetFromJsonAsync<GithubApiResponse>(apiUrl);
//        //[HttpGet]
//        //public async Task<IActionResult> GetSearchResults()
//        //{
//        //    var searchResults = await _dbContext.SearchResults.ToListAsync();
//        //    return Ok(searchResults);
//        //}

//        [HttpGet]
//        public async Task<IActionResult> GetSearchResults(int page = 1, int pageSize = 10)
//        {
//            var searchResults = await _dbContext.SearchResults
//                .Skip((page - 1) * pageSize)
//                .Take(pageSize)
//                .ToListAsync();

//            return Ok(searchResults);
//        }


//        [HttpDelete("{id:int}")]
//        public async Task<IActionResult> DeleteSearchResult(int id)
//        {
//            var searchResult = await _dbContext.SearchResults.FindAsync(id);

//            if (searchResult == null)
//            {
//                return NotFound();
//            }

//            _dbContext.SearchResults.Remove(searchResult);
//            await _dbContext.SaveChangesAsync();

//            return NoContent();
//        }
//    }
//}
