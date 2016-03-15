using System;

namespace Teachersteams.Domain.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException()
            : base() { }

        public DataNotFoundException(string message)
            : base(message) { }

        public DataNotFoundException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public DataNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        public DataNotFoundException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }
    }
}
