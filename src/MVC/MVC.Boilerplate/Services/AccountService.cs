using MVC.Boilerplate.Common.Helpers.ApiHelper;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Models.Account;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace MVC.Boilerplate.Services
{
    public class AccountService: IAccountService
    {
        private readonly IApiClient<LoginResponse> _apiClientLogin;
        private readonly IApiClient<Register> _apiClientRegister;
        private readonly ILogger<AccountService> _logger;


        public AccountService(IApiClient<LoginResponse> apiClientLogin, ILogger<AccountService> logger, IApiClient<Register> apiClientRegister)
        {
            _apiClientLogin = apiClientLogin;
            _logger = logger;
            _apiClientRegister = apiClientRegister;
        }
        

        public async Task<LoginResponse> Login(Login login)
        {
            _logger.LogInformation("LoginAccount Service initiated");
            var response = await _apiClientLogin.PostAuthAsync("Account/authenticate", login);
            if(response == null)
            {
                LoginResponse loginResponse = new LoginResponse();
                loginResponse.Message = $"Credentials for  { login.Email}  arent valid.";
                _logger.LogInformation("LoginAccount Service completed");
                return loginResponse;
            }
            _logger.LogInformation("LoginAccount Service conpleted");
            return response;
        }
        public async Task<Register> Register(Register register)
        {
            _logger.LogInformation("RegisterAccount Service initiated");
            var response = await _apiClientRegister.PostAuthAsync("Account/register", register);
            if (response == null)
            {
                //Register registerResponse = new Register();
                register.Message = $" UserName {register.UserName} or Email {register.Email} already exist";
                return register;
            }
            _logger.LogInformation("RegisterAccount Service conpleted");
            return response;

        }

        
    }
}
