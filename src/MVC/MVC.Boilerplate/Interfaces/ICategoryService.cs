using MVC.Boilerplate.Models.Category.Commands;
using MVC.Boilerplate.Models.Category.Queries;

namespace MVC.Boilerplate.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategories();
        Task<IEnumerable<Category>> GetAllCategoriesWithEvents(bool includeHistory);
        Task<Category> CreateCategory(CreateCategory createCategory);
    }
}
