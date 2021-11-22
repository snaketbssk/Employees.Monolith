using System;
using System.Runtime.Serialization;

namespace Employees.Monolith.LogicLayer.Exceptions.Entities
{
    public class ExistsUserException : Exception
    {
        public ExistsUserException()
        {
        }

        public ExistsUserException(string message) : base(message)
        {
        }

        public ExistsUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExistsUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
