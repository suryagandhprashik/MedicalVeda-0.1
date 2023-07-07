using Microsoft.AspNetCore.Mvc;
using MVC.Boilerplate.Models.GeneratePdf;
using Rotativa.AspNetCore;

namespace MVC.Boilerplate.Controllers
{
    public class GeneratePDFController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(Invoice.GetOne());
        }

        public IActionResult GeneratePdf()
        {
            return new ViewAsPdf(Invoice.GetOne());
        }
    }
}
