using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC.Boilerplate.Common.Helpers.EmailHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Common
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailClient, EmailClient>();
            return services;
        }
    }
}
