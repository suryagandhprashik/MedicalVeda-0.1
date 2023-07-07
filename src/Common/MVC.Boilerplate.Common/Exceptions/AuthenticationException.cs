using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Common.Exceptions
{
    [Serializable]
    public class AuthenticationException: ApplicationException
    {
        public AuthenticationException(string message) : base(message)
        {

        }
        protected AuthenticationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
