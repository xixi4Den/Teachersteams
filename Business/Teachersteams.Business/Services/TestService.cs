using System;
using System.Linq;
using Teachersteams.Domain;
using Teachersteams.Domain.Query;
using Group = Teachersteams.Domain.Entities.Group;

namespace Teachersteams.Business.Services
{
    public class TestService: ITestService
    {
        private readonly IUnitOfWork unitOfWork;

        public TestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public int AddTwo(int num)
        {
            var getAll = unitOfWork.GetAll<Group>();
            return 2 + num;
        }
    }
}