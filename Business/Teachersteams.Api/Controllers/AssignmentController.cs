using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services;
using Teachersteams.Business.ViewModels.Assignment;
using Teachersteams.Business.ViewModels.Grid;

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

        [HttpGet]
        public HttpResponseMessage Download(FileType fileType, string file)
        {
            var task = Task.Run(() => fileManager.Download("Assignments", file));
            var buffer = task.Result;
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(buffer)
            };
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = file,
            };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [HttpPost]
        public HttpResponseMessage Post(string userId, [FromBody]AssignmentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var assignment = assignmentService.CreateAssignment(userId, viewModel);
                return Request.CreateResponse(HttpStatusCode.Created, assignment);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        public HttpResponseMessage GetAll(Guid groupId, string userId, [FromUri]GridOptions options)
        {
            var teachers = assignmentService.GetAllAssignments(groupId, options);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }

        [HttpGet]
        public HttpResponseMessage Count(Guid groupId, string userId)
        {
            var teachers = assignmentService.AssignmentCount(groupId);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }
    }
}
