using System;
using System.Runtime.Serialization;

namespace Employees.Monolith.Api.Controllers.TokenControllers.Exceptions.Entities
{
    public class WrongPasswordUserException : Exception
    {
        public WrongPasswordUserException()
        {
        }

        public WrongPasswordUserException(string message) : base(message)
        {
        }

        public WrongPasswordUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongPasswordUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
