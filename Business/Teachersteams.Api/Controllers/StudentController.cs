using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teachersteams.Business.Services;
using Teachersteams.Business.ViewModels;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;

namespace Teachersteams.Api.Controllers
{
    public class StudentController : ApiController
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpPost]
        public HttpResponseMessage Post(string userId, [FromBody]StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = studentService.Invite(viewModel);
                return Request.CreateResponse(HttpStatusCode.Created, teacher);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid groupId, string userId, [FromUri]GridOptions options)
        {
            var teachers = studentService.GetUsers(groupId, options);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }

        [HttpGet]
        public HttpResponseMessage Count(Guid groupId, string userId)
        {
            var teachers = studentService.Count(groupId);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }

        [HttpGet]
        public HttpResponseMessage Requests(string userId)
        {
            var requests = studentService.GetRequests(userId);
            return Request.CreateResponse(HttpStatusCode.OK, requests);
        }

        [HttpGet]
        public HttpResponseMessage DoesHaveRequest(string userId, Guid groupId)
        {
            var requests = studentService.DoesHaveRequest(userId, groupId);
            return Request.CreateResponse(HttpStatusCode.OK, requests);
        }

        [HttpPost]
        public HttpResponseMessage Response([FromBody]RequestViewModel viewModel, string userId)
        {
            viewModel.UidTo = userId;
            studentService.Response(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, ModelState);
        }
    }
}
