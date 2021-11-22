using System;
using System.Runtime.Serialization;

namespace Employees.Monolith.Api.Controllers.TokenControllers.Exceptions.Entities
{
    public class SignInBotUserException : Exception
    {
        public SignInBotUserException()
        {
        }

        public SignInBotUserException(string message) : base(message)
        {
        }

        public SignInBotUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SignInBotUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
