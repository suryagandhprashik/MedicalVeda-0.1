using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Common.Helpers.EmailHelper
{
    public interface IEmailClient
    {
        Task<bool> SendEmail(Email email);
    }
}
