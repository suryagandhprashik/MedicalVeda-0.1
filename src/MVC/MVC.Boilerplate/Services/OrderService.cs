using MVC.Boilerplate.Models.Order;
using MVC.Boilerplate.Common.Helpers.ApiHelper;
using MVC.Boilerplate.Interfaces;
using MVC.Boilerplate.Common.Models.Responses;

namespace MVC.Boilerplate.Services
{
    public class OrderService: IOrderService
    {
        private readonly IApiClient<Orders> _client;
        public readonly ILogger<CategoryService> _logger;

        public OrderService(IApiClient<Orders> client, ILogger<CategoryService> logger)
        {
            _client = client;
            _logger = logger;
        }
        public async Task<PagedResponse<IEnumerable<Orders>>> GetOrderList(string date, int page, int pageSize)
        {
            _logger.LogInformation("GetOrderList Service initiated.");
            //var orders = await _client.GetPagedAsync("Order?date=2022-02-21&page=" + page + "&size=" + pageSize);
            var orders = await _client.GetPagedAsync("Order?date=" + date + " &page=" + page + "&size=" + pageSize);
            _logger.LogInformation("GetOrderList Service completed.");
            return orders;
        }
    }
}
