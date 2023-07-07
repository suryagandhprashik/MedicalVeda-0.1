using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Category.Commands;
using MVC.Boilerplate.Models.Category.Queries;
using Rotativa.AspNetCore;

namespace MVC.Boilerplate.Controllers
{
    [Route("Category")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;
        private readonly INotyfService _notyf;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger, INotyfService notyf)
        {
            _categoryService = categoryService;
            _logger = logger;
            _notyf = notyf;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Index Action initiated");
            var response = await _categoryService.GetAllCategories();
            _logger.LogInformation("Index Action completed");
            return View(response);
        }

        [HttpGet("CategoriesWithEvents")]
        public async Task<IActionResult> CategoriesWithEvents(bool includeHistory)
        {
            _logger.LogInformation("CategoriesWithEvents Action initiated");
            var response = await _categoryService.GetAllCategoriesWithEvents(includeHistory);
            _logger.LogInformation("CategoriesWithEvents Action initiated");
            return View(response);
        }

        [HttpGet("CreateCategory")]
        public IActionResult CreateCategory()   
        {
            return View();
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategory request)
        {
            _logger.LogInformation("CreateCategory Action initiated");
            var response = await _categoryService.CreateCategory(request);
            _notyf.Success("Category Created Successfully");
            _logger.LogInformation("CreateCategory Action initiated");
            return RedirectToAction("Index");
        }


        [HttpGet("GenerateAllCategoriesPdf")]
        public async Task<IActionResult> GenerateAllCategoriesPdf()
        {
            var categories =  await _categoryService.GetAllCategories();
            return new ViewAsPdf(categories);
        }
    }
}
