using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Common.Exceptions;

namespace MVC.Boilerplate.Controllers
{
    //[Route("Error")]
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }
        [Route("ErrorHandler/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            _logger.LogInformation("HttpStatusCode action method initiated");
            var statusCodeResult = HttpContext.Response.StatusCode;
            switch (statusCode)
            {
                case 401:
                    return RedirectToAction("Error401");
                    break;
                case 404:
                    return RedirectToAction("Error404");
                    break;
                case 500:
                    return RedirectToAction("Error500");
                    break;
                case 503:
                    return RedirectToAction("Error503");
                    break;
            }
            return View("Default");
        }
        public IActionResult Error401()
        {
            _logger.LogInformation("Error401 action method initiated");
            return View("Error401");
        }
        public IActionResult Error404()
        {
            _logger.LogInformation("Error404 action method initiated");
            return View("Error404");
        }
        public IActionResult Error500()
        {
            _logger.LogInformation("Error405 action method initiated");
            return View("Error500");
        }

        //Account error

        public IActionResult Error503()
        {
            _logger.LogInformation("Error action method initiated");
            return View("Error503");
        }

        public IActionResult Default()
        {
            _logger.LogInformation("Default action method initiated");
            return View("Default");
        }
        public IActionResult Raise401Ex()
        {
            _logger.LogInformation("Raise401Ex action initaited and unauthorized exception raised");
            throw new UnauthorizedAccessException();
        }
        public IActionResult Raise404Ex()
        {
            _logger.LogInformation("Raise404Ex action initaited and not found exception raised");
            throw new NotFoundException("Page not found", 404);
        }
        public IActionResult Raise500Ex()
        {
            _logger.LogInformation("Raise500Ex action initaited and internal server error exception raised");
            throw new Exception();
        }
        public IActionResult DefaultEx()
        {
            _logger.LogInformation("DefaultEx action initaited and internal server error exception raised");
            throw new NotImplementedException();
        }
    }
}
