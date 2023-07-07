using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Common.Exceptions
{
    [Serializable]
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string message) : base(message)
        {
        }

        protected BadRequestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
