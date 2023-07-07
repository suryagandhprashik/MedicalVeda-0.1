using Microsoft.AspNetCore.Mvc;

namespace MVC.Boilerplate.Middleware
{
    public class AuthMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path != "/")
            {
                if (context.Request.Path != "/Account/Register")
                {
                    //string t = context.Session.GetString("Username");
                    var t3 = context.Session.Keys.ToList();
                    foreach (var key in t3)
                        if (key == "UserName")
                            goto Skip; ;
                    //If session with UserName doesn't exists 
                    context.Response.Clear();
                    context.Response.StatusCode = 503;
                    context.Response.ContentType = "application/json";
                    context.Response.Redirect("/");
                }
            }
            Skip:
                await _next(context);
        }
    }
}
