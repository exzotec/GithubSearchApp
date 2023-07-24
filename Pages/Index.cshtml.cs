using GithubSearchApp.Data;
using GithubSearchApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace GitHubSearch.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AppDbContext _dbContext;

        [BindProperty]
        public string? SearchText { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory, AppDbContext dbContext)
        {
            _httpClientFactory = httpClientFactory;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Проверяем наличие результатов поиска в локальной базе данных
            var searchResult = await _dbContext.SearchResults.FirstOrDefaultAsync(s => s.SearchText == SearchText);

            // Если результаты поиска не найдены, отправляем запрос на API GitHub
            if (searchResult == null)
            {
                try
                {
                    var httpClient = _httpClientFactory.CreateClient("GitHub");
                    var apiUrl = $"https://api.github.com/search/repositories?q={SearchText}";

                    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, apiUrl) { };
                    var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var response = await httpResponseMessage.Content.ReadAsStringAsync();
                        var _ListResponse = JsonConvert.DeserializeObject<GH_ListResponse>(response);

                        //Сохраняем полученные результаты в локальной базе данных
                        if (_ListResponse != null)
                        {
                            foreach (var item in _ListResponse.Items)
                            {
                                //// Создаем экземпляр класса SearchResult и заполняем его данными из сереализованного обьекта
                                searchResult = new SearchResult
                                {
                                    SearchText = SearchText,
                                    JsonContent = response
                                };

                                // Добавляем экземпляр SearchResult в контекст базы данных
                                _dbContext.SearchResults.Add(searchResult);
                            }
                        }
                        try
                        {
                            //сохраняем изменения
                            await _dbContext.SaveChangesAsync();
                            Console.WriteLine("Объект успешно сохранен в базе данных!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка при сохранении объекта: {ex.Message}");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Ошибка HTTP: {ex.Message}");
                }
                catch (Newtonsoft.Json.JsonException ex)
                {
                    Console.WriteLine($"Ошибка десериализации JSON: {ex.Message}");
                }
            }

            // Если данные найдены в базе данных, перенаправляем на страницу с результатами
            if (searchResult != null && !string.IsNullOrEmpty(searchResult.JsonContent))
            {
                var _ListResponse = JsonConvert.DeserializeObject<GH_ListResponse>(searchResult.JsonContent);
                return RedirectToPage("Cards", new { searchText = SearchText });
            }
            else
            {
                // В данном случае результаты не были найдены, поэтому мы останемся на странице Index и выведем соответствующее сообщение.
                ModelState.AddModelError(string.Empty, "Результаты не найдены");
                return Page();
            }

        }
    }
}