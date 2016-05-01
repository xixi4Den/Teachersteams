using Dropbox.Api;
using System.IO;
using System.Threading.Tasks;
using Dropbox.Api.Files;


namespace Teachersteams.Business.Services
{
    public class FileManager : IFileManager
    {
        private string accessToken = "LqNeLQrmUQwAAAAAAAADaWA77LfHlZjcxkqiBctnlHZTqY2pHxca1XFtac1kLTq2";

        public async Task<string> Upload(string folder, string file, Stream stream)
        {
            using (var dbx = new DropboxClient(accessToken))
            {
                var path = FormatPath(folder, file);
                var result = await dbx.Files.UploadAsync(path, WriteMode.Overwrite.Instance, body: stream);
                return result.Name;
            }
        }

        public async Task<byte[]> Download(string folder, string file)
        {
            using (var dbx = new DropboxClient(accessToken))
            {
                var path = FormatPath(folder, file);

                using (var response = await dbx.Files.DownloadAsync(path))
                {
                    var result = await response.GetContentAsByteArrayAsync();
                    return result;
                }
            }
        }

        private static string FormatPath(string folder, string file)
        {
            return "/" + folder + "/" + file;
        }
    }
}
