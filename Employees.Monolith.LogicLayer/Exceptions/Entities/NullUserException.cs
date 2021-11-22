using System;
using System.Runtime.Serialization;

namespace Employees.Monolith.LogicLayer.Exceptions.Entities
{
    public class NullUserException : Exception
    {
        public NullUserException()
        {
        }

        public NullUserException(string message) : base(message)
        {
        }

        public NullUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NullUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
