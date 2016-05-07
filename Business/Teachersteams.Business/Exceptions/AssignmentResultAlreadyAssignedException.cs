namespace Teachersteams.Business.Exceptions
{
    public class AssignmentResultAlreadyAssignedException: BusinessException
    {
        private const string message = "The assignment result has already been assigned to other teacher. You cannot assign it to muself.";

        public AssignmentResultAlreadyAssignedException()
            : base(message)
        { 
        }
    }
}
