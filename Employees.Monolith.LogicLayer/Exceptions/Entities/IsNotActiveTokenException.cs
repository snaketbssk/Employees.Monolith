using System;
using System.Runtime.Serialization;

namespace Employees.Monolith.LogicLayer.Exceptions.Entities
{
    public class IsNotActiveTokenException : Exception
    {
        public IsNotActiveTokenException()
        {
        }

        public IsNotActiveTokenException(string message) : base(message)
        {
        }

        public IsNotActiveTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected IsNotActiveTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
