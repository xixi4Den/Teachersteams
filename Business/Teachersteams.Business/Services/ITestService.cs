using Shared.Framework.Dependency;

namespace Teachersteams.Business.Services
{
    public interface ITestService: IRequestDependency
    {
        int AddTwo(int num);
    }
}
