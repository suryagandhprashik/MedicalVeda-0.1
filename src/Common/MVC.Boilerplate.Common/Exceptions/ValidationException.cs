using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Boilerplate.Common.Exceptions
{
    [Serializable]
    public class ValidationException : ApplicationException
    {
        public List<string> ValdationErrors { get; set; }

        public ValidationException(ValidationResult validationResult)
        {
            ValdationErrors = new List<string>();

            foreach (var validationError in validationResult.Errors)
            {
                ValdationErrors.Add(validationError.ErrorMessage);
            }
        }

        protected ValidationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
