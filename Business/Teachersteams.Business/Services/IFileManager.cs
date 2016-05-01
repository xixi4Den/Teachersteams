using System.IO;
using System.Threading.Tasks;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services
{
    public interface IFileManager: IRequestDependency
    {
        Task<string> Upload(string folder, string file, Stream stream);
    }
}
