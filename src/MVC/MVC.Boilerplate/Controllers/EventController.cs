using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Event.Commands;
using MVC.Boilerplate.Models.Event.Queries;

namespace MVC.Boilerplate.Controllers
{
    public class EventController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly INotyfService _notyf;
        private readonly IEventService _eventService;
        public EventController(IEventService eventService, INotyfService notyf, ICategoryService categoryService)
        {
            _eventService = eventService;
            _categoryService = categoryService;
            _notyf = notyf;
           
        }
        public async Task<IActionResult> GetEvents()
        {
            var result = await _eventService.GetEventList();
            return View(result);
        }
        [HttpGet]
        public async Task<IActionResult> CreateEvent()
        {
            var categories = await _categoryService.GetAllCategories();
            ViewBag.categoryId = categories;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateEvent(CreateEvent events)
        {
            if (ModelState.IsValid)
            {
                if (events.Date <= DateTime.Now)
                {
                    var categories = await _categoryService.GetAllCategories();
                    ViewBag.categoryId = categories;
                    _notyf.Error("DateTime should be greater than current dateTime");
                    return View();
                }
                else
                {
                    var result = await _eventService.CreateEvent(events);
                    var eventResult = await _eventService.GetEventList();
                    _notyf.Success("Event created successfully");
                    return View("GetEvents", eventResult);
                }
                
            }
            else
            {
                var categories = await _categoryService.GetAllCategories();
                ViewBag.categoryId = categories;
                return View();
            }
            
        }

        public async Task<IActionResult> GetEventById(string eventId)
        {
            var result = await _eventService.GetEventById(eventId);
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEvent(string id)
        {
            var result = await _eventService.GetEventById(id);
            var categories = await _categoryService.GetAllCategories();
            ViewBag.categoryId = categories;
            return View(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEvent(GetByIdEvent updateEvent)
        {
            if(ModelState.IsValid)
            {
                if (updateEvent.Date <= DateTime.Now)
                {
                    var categories = await _categoryService.GetAllCategories();
                    ViewBag.categoryId = categories;
                    _notyf.Error("DateTime should be greater than current dateTime");
                    return View();
                }
                else
                {
                    var result = await _eventService.UpdateEvent(updateEvent);
                    _notyf.Success("Event updated successfully");
                    return RedirectToAction("UpdateEvent");
                }
               
            }
            else
            {
                var categories = await _categoryService.GetAllCategories();
                ViewBag.categoryId = categories;
                return View();
            }
            
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            var res = await _eventService.DeleteEvent(eventId);

            if (res!= "")
            {
                _notyf.Error("Something went wrong ...");//"Something went wrong ..."
            }
            else
            {
                _notyf.Success("Record Deleted Successfully");
                /*  return RedirectToAction("Index", "DeleteModal");*/
            }
            var result = await _eventService.GetEventList();
            return View("GetEvents",result);
        }
    }
}
