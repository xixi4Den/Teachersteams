using System;

namespace Teachersteams.Shared.Validation
{
    public static class Contract
    {
        /// <summary>
        /// Intended to check input parameter.
        /// </summary>
        /// <typeparam name="TException">Type of exception to throw.</typeparam>
        /// <param name="condition">Check condition.</param>
        /// <param name="parameterName">Name of input parameter.</param>
        public static void Requires<TException>(bool condition, string parameterName = null)
            where TException : ArgumentException
        {
            Assert<TException>(condition, parameterName);
        }

        /// <summary>
        /// Intended to check condition inside method body.
        /// </summary>
        /// <typeparam name="TException">Type of exception to throw.</typeparam>
        /// <param name="condition">Check condition.</param>
        /// <param name="message">Exception message.</param>
        public static void Assert<TException>(bool condition, string message = null)
            where TException : Exception
        {
            if (condition)
            {
                return;
            }

            var exception = CreateException<TException>(message);
            throw exception;
        }

        private static TException CreateException<TException>(string parameter)
        {
            if (parameter == null)
            {
                return Activator.CreateInstance<TException>();
            }

            // specific constructor
            if (typeof (TException) == typeof (ArgumentException))
            {
                return (TException)Activator
                    .CreateInstance(typeof (TException), string.Format("Parameter '{0}' didn't pass validation.", parameter), parameter);
            }

            return (TException)Activator.CreateInstance(typeof (TException), parameter);
        }
    }
}
