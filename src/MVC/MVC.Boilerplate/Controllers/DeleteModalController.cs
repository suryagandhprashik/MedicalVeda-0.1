using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Boilerplate.Controllers
{
    public class DeleteModalController : Controller
    {
        private readonly INotyfService _notyf;
        public DeleteModalController(INotyfService notyf)
        {
            _notyf = notyf;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Delete(int id)
        {

            if (id == 0)
            {
                _notyf.Error("Something went wrong ...");//"Something went wrong ..."
            }
            else
            {
                _notyf.Success("Record Deleted Successfully");
              /*  return RedirectToAction("Index", "DeleteModal");*/
            }


            return Ok();
        }

    }
}
