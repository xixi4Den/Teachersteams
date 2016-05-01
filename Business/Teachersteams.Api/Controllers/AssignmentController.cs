using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services;

namespace Teachersteams.Api.Controllers
{
    public class AssignmentController : ApiController
    {
        private readonly IFileManager fileManager;

        public AssignmentController(IFileManager fileManager)
        {
            this.fileManager = fileManager;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Upload(FileType fileType)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "media type");
            }

            var provider = new MultipartDropboxProvider(fileManager);

            var result = await Request.Content.ReadAsMultipartAsync(provider);

            return Request.CreateResponse(HttpStatusCode.OK, result);
            //    .ContinueWith(t =>
            //{
            //    if (t.IsFaulted)
            //    {
            //        throw t.Exception;
            //    }

            //    return t.Result.FileName;
            //});

            //foreach (var stream in provider.Contents)
            //{
            //    var folder = ResolveFolder(fileType);
            //    var uniqueFileName = FormUniqueFileName(stream);
            //    var data = await stream.ReadAsStreamAsync();
            //    var result = await fileManager.Upload(folder, uniqueFileName, data);
            //    return Request.CreateResponse(HttpStatusCode.OK, result);
            //}
            //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "unexpected");
        }

        private string ResolveFolder(FileType fileType)
        {
            if (fileType == FileType.Assignment)
            {
                return "Assignments";
            }
            if (fileType == FileType.Result)
            {
                return "Results";
            }
            throw new ArgumentOutOfRangeException();
        }

        private static string FormUniqueFileName(HttpContent stream)
        {
            var uniqueFileName = Guid.NewGuid().ToString();

            var ext = GetExtensionOfOriginalFile(stream);
            if (ext != null)
            {
                uniqueFileName = Path.ChangeExtension(uniqueFileName, ext);
            }
            return uniqueFileName;
        }

        private static string GetExtensionOfOriginalFile(HttpContent stream)
        {
            var originalFileName = stream.Headers.ContentDisposition.FileName;
            originalFileName = originalFileName.Replace("\"", "");
            return Path.GetExtension(originalFileName);
        }
    }
}
