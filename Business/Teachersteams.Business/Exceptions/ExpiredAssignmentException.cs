namespace Teachersteams.Business.Exceptions
{
    public class ExpiredAssignmentException: BusinessException
    {
        private const string messagePattern = "The assignment has already been expired. You cannot send your answers";

        public ExpiredAssignmentException()
            : base(messagePattern)
        { 
        }
    }
}
