using MVC.Boilerplate.Models.Lazy;

namespace MVC.Boilerplate.Interfaces
{
    public interface ILazyService
    {
        Task<List<Person>> PersonList();
        Task<List<Animal>> AnimalList();
        Task<int> AnimalsCount();
    }
}
