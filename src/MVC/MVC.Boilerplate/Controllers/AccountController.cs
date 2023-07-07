using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MVC.Boilerplate.Common.Helpers.EmailHelper;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Account;


namespace MVC.Boilerplate.Controllers
{
    public class AccountController : Controller
    {
        public IEmailClient _emailService;
        private readonly INotyfService _notyf;
        private readonly IAccountService _service;

        public AccountController(IEmailClient emailService,INotyfService notfy, IAccountService service)
        {
            _notyf = notfy;
            _emailService = emailService;
            _service=service;
        }

        public IActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> LoginAsync(Login login)
        {

            if (ModelState.IsValid)
            {
           
                LoginResponse loginResponse = await _service.Login(login);
                if (loginResponse.UserName != null)
                {
                    HttpContext.Session.SetString("UserName", loginResponse.UserName);
                    _notyf.Success("Logged In Successfully");
                    // Email Configuration
                    Email _email = new Email();
                    _email.To = loginResponse.Email;
                    _email.Body = "You logged in Successfully";
                    _email.Subject = "Confirmation Email";
                    _emailService.SendEmail(_email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                   
                    _notyf.Error(loginResponse.Message);
                    return View();
                }

            }
            else
            {
                return View();
            }           
        }*/
        [HttpPost]
        public async Task<IActionResult> LoginAsync(Login login)
        {

            if (ModelState.IsValid)
            {
                LoginResponse? loginResponse = null;
                if (login.Email.Equals("prashik@gmail.com") && login.Password.Equals("Prashik@123"))
                {
                    loginResponse = new()
                    {
                        Id = "1",
                        IsAuthenticated = true,
                        UserName = "Prashik",
                        Email = "prashik@gmail.com",
                        Password = "Prashik@123",
                        Token = "ffscas",

                    };
                }
                //LoginResponse loginResponse = await _service.Login(login);

                if (loginResponse.UserName != null)
                {
                    HttpContext.Session.SetString("UserName", loginResponse.UserName);
                    _notyf.Success("Logged In Successfully");
                    var data = HttpContext.Session.GetString("UserName");

                    // Email Configuration
                    //Email _email = new Email();
                    //_email.To = loginResponse.Email;
                    //_email.Body = "You logged in Successfully";
                    //_email.Subject = "Confirmation Email";
                    //_emailService.SendEmail(_email);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    _notyf.Error(loginResponse.Message);
                    return View();
                }

            }
            else
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            _notyf.Success("Logged Out Successfully");
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {


            if (ModelState.IsValid)
            { //checking model state
                
               await _service.Register(register);
                if (register.Message == null)
                {
                    // Email
                    Email _email = new Email();
                    _email.To = register.Email;
                    _email.Body = "You Registered Successfully";
                    _email.Subject = "Confirmation Email";
                    var result = _emailService.SendEmail(_email);
                    _notyf.Success("Registered Successfully");

                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    _notyf.Error(register.Message);
                    return View();

                }

            }
            else
            {
                return View();
            }
       

        }
    }
    }

