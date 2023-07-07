using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Lazy;

namespace MVC.Boilerplate.Controllers
{
    [Route("[controller]/[action]")]
    public class LazyController : Controller
    {
        private readonly ILogger<LazyController> _logger;
        private readonly ILazyService _lazyService;
        int RecordsPerPage = 20;
        int RecordSizeForAnimals = 10;
        List<Person> PersonList;
        public LazyController(ILogger<LazyController> logger,ILazyService lazyService)
        {
            _logger = logger;
            _lazyService = lazyService;
        }
        [HttpGet]
        public async Task<IActionResult> LoadPersonList([FromQuery(Name = "pageNum")] int pageNum = 0)
        {
            _logger.LogInformation("LoadList Action initiated");
            //Checks for Ajax Request
            if(Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                var person = await GetPersonPageData(pageNum);
                _logger.LogInformation("LoadList Action completed");
                return PartialView("_PersonData", person);
            }
            else
            {

                ViewBag.RecordsPerPage = RecordsPerPage;
                ViewBag.Persons = await GetPersonPageData(pageNum);
                ViewBag.TotalPersonCount = PersonList.Count;
                ViewBag.MaxPageCount = (PersonList.Count / RecordsPerPage);

                _logger.LogInformation("LoadList Action completed");
                return View("Index");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> LoadAnimalList()
        {
            // ceil value of totalAnimals divided by RecordSizeForAnimals
            ViewBag.TableCount = Math.Ceiling(((double)await _lazyService.AnimalsCount() / RecordSizeForAnimals)) ;
            return View();
        }

        [HttpGet]
        public IActionResult AnimalViewComponent(int componentNum)
        {
            return ViewComponent("AnimalList", new { componentNum = componentNum });
        }


        async Task<List<Person>> GetPersonPageData(int pageNum)
        {
            PersonList = await _lazyService.PersonList();
            //It defines from where in PersonList records should be fetched
            int from = pageNum * RecordsPerPage;

            var selectedData = PersonList.Skip(from-1).Take(RecordsPerPage).ToList();
            return selectedData;
        }
    }
}
