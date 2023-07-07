using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;

namespace MVC.Boilerplate.Models.DataTableProcessing
{
    public class DataTableModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var request = bindingContext.HttpContext.Request;

            // Retrieve request data
            var draw = Convert.ToInt32(request.Query["draw"]);
            var start = Convert.ToInt32(request.Query["start"]);
            var length = Convert.ToInt32(request.Query["length"]);

            // Search
            var search = new Search
            {
                Value = request.Query["search[value]"],
                Regex = Convert.ToBoolean(request.Query["search[regex]"])
            };

            // Order
            var o = 0;
            var order = new List<ColumnOrder>();
            while (!StringValues.IsNullOrEmpty(request.Query["order[" + o + "][column]"]))
            {
                order.Add(new ColumnOrder
                {
                    Column = Convert.ToInt32(request.Query["order[" + o + "][column]"]),
                    Dir = request.Query["order[" + o + "][dir]"]
                });
                o++;
            }

            // Columns
            var c = 0;
            var columns = new List<Column>();
            while (!StringValues.IsNullOrEmpty(request.Query["columns[" + c + "][name]"]))
            {
                columns.Add(new Column
                {
                    Data = request.Query["columns[" + c + "][data]"],
                    Name = request.Query["columns[" + c + "][name]"],
                    Orderable = Convert.ToBoolean(request.Query["columns[" + c + "][orderable]"]),
                    Searchable = Convert.ToBoolean(request.Query["columns[" + c + "][searchable]"]),
                    Search = new Search
                    {
                        Value = request.Query["columns[" + c + "][search][value]"],
                        Regex = Convert.ToBoolean(request.Query["columns[" + c + "][search][regex]"])
                    }
                });
                c++;
            }

            var result = new DataTablesResult
            {
                Draw = draw,
                Start = start,
                Length = length,
                Search = search,
                Order = order,
                Columns = columns
            };

            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
