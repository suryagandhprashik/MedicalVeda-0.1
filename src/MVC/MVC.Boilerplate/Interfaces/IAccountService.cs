using MVC.Boilerplate.Models.Account;

namespace MVC.Boilerplate.Interfaces
{
    public interface IAccountService
    {
        Task<LoginResponse> Login (Login login);
        Task<Register> Register(Register register);

        
    }
}
