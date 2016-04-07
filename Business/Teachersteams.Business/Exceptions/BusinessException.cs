using System;

namespace Teachersteams.Business.Exceptions
{
    /// <summary>
    /// Base class for business exceptions. Business exceptions should provide 
    /// short business specific information for user and hide implementation details.
    /// </summary>
    public class BusinessException: Exception
    {
        public BusinessException()
            : base() { }

        public BusinessException(string message)
            : base(message) { }
    }
}
