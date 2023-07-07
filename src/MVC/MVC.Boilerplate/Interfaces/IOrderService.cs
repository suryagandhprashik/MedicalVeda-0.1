using MVC.Boilerplate.Common.Models.Responses;
using MVC.Boilerplate.Models.Order;

namespace MVC.Boilerplate.Interfaces
{
    public interface IOrderService
    {
        Task<PagedResponse<IEnumerable<Orders>>> GetOrderList(string date, int page, int pageSize);
    }
}
