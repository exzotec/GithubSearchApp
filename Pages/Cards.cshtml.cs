using GithubSearchApp.Data.Models;
using GithubSearchApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using GitHubSearch.Pages;

namespace GithubSearchApp.Pages
{
    public class CardsModel : PageModel
    {
        private readonly AppDbContext _dbContext;

        [BindProperty(SupportsGet = true)]
        public string? SearchText { get; set; }


        public GH_ListResponse Results { get; set; }
        public List<GH_ItemResponse> Items { get; set; }

        public CardsModel(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Получите результат поиска из базы данных на основе SearchText
            var searchResult = await _dbContext.SearchResults.FirstOrDefaultAsync(s => s.SearchText == SearchText);

            if (searchResult != null && !string.IsNullOrEmpty(searchResult.JsonContent))
            {
                // Десериализуйте JSON-контент для получения результатов
                Results = JsonConvert.DeserializeObject<GH_ListResponse>(searchResult.JsonContent);
                return Page();
            }
            else
            {
                // Результат поиска не найден в базе данных или JSON-контент пустой.
                return RedirectToPage("Index");
            }
        } 
    }
}
