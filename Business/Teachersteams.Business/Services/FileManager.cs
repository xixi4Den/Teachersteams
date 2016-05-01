using Dropbox.Api;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api.Files;


namespace Teachersteams.Business.Services
{
    public class FileManager : IFileManager
    {
        public async Task<string> Upload(string folder, string file, Stream stream)
        {
            using (var dbx = new DropboxClient("LqNeLQrmUQwAAAAAAAADaWA77LfHlZjcxkqiBctnlHZTqY2pHxca1XFtac1kLTq2"))
            {
                var path = "/" + folder + "/" + file;
                var result = await dbx.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: stream);
                return result.Name;
            }
        }
    }
}
