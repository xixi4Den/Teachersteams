using System.IO;
using System.Threading.Tasks;
using Teachersteams.Business.Enums;
using Teachersteams.Shared.Dependency;

namespace Teachersteams.Business.Services.Contracts
{
    public interface IFileManager: IRequestDependency
    {
        Task<string> Upload(string folder, string file, Stream stream);

        Task<byte[]> Download(FileType fileType, string file);
    }
}
