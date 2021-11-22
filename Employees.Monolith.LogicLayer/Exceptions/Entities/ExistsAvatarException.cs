using System;
using System.Runtime.Serialization;

namespace Employees.Monolith.LogicLayer.Exceptions.Entities
{
    public class ExistsAvatarException : Exception
    {
        public ExistsAvatarException()
        {
        }

        public ExistsAvatarException(string message) : base(message)
        {
        }

        public ExistsAvatarException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ExistsAvatarException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
