using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services;
using Teachersteams.Business.Services.Contracts;
using Teachersteams.Shared.Validation;

namespace Teachersteams.Api
{
    public class MultipartDropboxProvider: MultipartMemoryStreamProvider
    {
        private readonly IFileManager fileManager;

        public FileType FileType { get; set; }

        public MultipartDropboxProvider(IFileManager fileManager, FileType fileType) 
        {
            this.fileManager = fileManager;
            FileType = fileType;
        }

        public string FileName { get; set; }

        public override async Task ExecutePostProcessingAsync()
        {
            Contract.Assert<InvalidOperationException>(Contents.Count == 1);

            var content = Contents.Single();
            var folder = ResolveFolder();
            var uniqueFileName = FormUniqueFileName(content);
            FileName = await fileManager.Upload(folder, uniqueFileName, await content.ReadAsStreamAsync());

            await base.ExecutePostProcessingAsync();
        }

        private string ResolveFolder()
        {
            if (FileType == FileType.Assignment)
            {
                return "Assignments";
            }
            if (FileType == FileType.Result)
            {
                return "Results";
            }
            throw new ArgumentOutOfRangeException();
        }

        private static string FormUniqueFileName(HttpContent content)
        {
            var uniqueFileName = Guid.NewGuid().ToString();

            var ext = GetExtensionOfOriginalFile(content);
            if (ext != null)
            {
                uniqueFileName = Path.ChangeExtension(uniqueFileName, ext);
            }
            return uniqueFileName;
        }

        private static string GetExtensionOfOriginalFile(HttpContent content)
        {
            var originalFileName = content.Headers.ContentDisposition.FileName;
            originalFileName = originalFileName.Replace("\"", "");
            return Path.GetExtension(originalFileName);
        }
    }
}