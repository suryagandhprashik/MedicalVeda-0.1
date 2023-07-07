using MVC.Boilerplate.Common.Helpers.ApiHelper;
using MVC.Boilerplate.Models.Category.Queries;
using MVC.Boilerplate.Models.Category.Commands;
using MVC.Boilerplate.Interfaces;

namespace MVC.Boilerplate.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IApiClient<Category> _client;
        public readonly ILogger<CategoryService> _logger;

        public CategoryService(IApiClient<Category> client,ILogger<CategoryService> logger)
        {
            _client = client;
            _logger = logger;
        }


        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            _logger.LogInformation("GetAllCategories Service initiated");
            var Categories = await _client.GetAllAsync("Category/all");
            _logger.LogInformation("GetAllCategories Service conpleted");
            return Categories.Data;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesWithEvents(bool includeHistory)
        {
            _logger.LogInformation("GetAllCategoriesWithEvents Service initiated");
            //var url = includeHistory ? "Category/allwithevents?includeHistory=true" : "Category/allwithevents?includeHistory=false";
            var Categories = await _client.GetAllAsync($"Category/allwithevents?includeHistory={includeHistory}");
            _logger.LogInformation("GetAllCategoriesWithEvents Service conpleted");
            return Categories.Data;
        }
        
        public async Task<Category> CreateCategory(CreateCategory createCategory)
        {
            _logger.LogInformation("CreateCategory Service initiated");
            var Categories = await _client.PostAsync("Category",createCategory);
            _logger.LogInformation("CreateCategory Service conpleted");
            return Categories.Data;
        }

    }
}
