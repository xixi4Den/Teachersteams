using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services;
using Teachersteams.Business.ViewModels.Assignment;

namespace Teachersteams.Api.Controllers
{
    public class AssignmentController : ApiController
    {
        private readonly IFileManager fileManager;
        private readonly IAssignmentService assignmentService;

        public AssignmentController(IFileManager fileManager, IAssignmentService assignmentService)
        {
            this.fileManager = fileManager;
            this.assignmentService = assignmentService;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Upload(FileType fileType)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "media type");
            }
            
            try
            {
                var provider = new MultipartDropboxProvider(fileManager, fileType);
                var result = await Request.Content.ReadAsMultipartAsync(provider);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "file uploading");
            }
        }

        [HttpPost]
        public HttpResponseMessage Post(string userId, [FromBody]AssignmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var assignment = assignmentService.Create(userId, viewModel);
                return Request.CreateResponse(HttpStatusCode.Created, assignment);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
