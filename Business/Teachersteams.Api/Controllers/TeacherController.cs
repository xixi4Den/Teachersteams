using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services;
using Teachersteams.Business.ViewModels;
using Teachersteams.Business.ViewModels.Grid;
using Teachersteams.Business.ViewModels.User;

namespace Teachersteams.Api.Controllers
{
    public class TeacherController : ApiController
    {
        private readonly ITeacherService teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            this.teacherService = teacherService;
        }

        [HttpPost]
        public HttpResponseMessage Post(string userId, [FromBody]TeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = teacherService.Invite(viewModel);
                return Request.CreateResponse(HttpStatusCode.Created, teacher);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpGet]
        public HttpResponseMessage Get(Guid groupId, UserType userType, string userId, [FromUri]GridOptions options)
        {
            var teachers = teacherService.GetUsers(groupId, options, userType);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }

        [HttpGet]
        public HttpResponseMessage Count(Guid groupId, string userId)
        {
            var teachers = teacherService.Count(groupId);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }

        [HttpGet]
        public HttpResponseMessage Requests(string userId)
        {
            var requests = teacherService.GetRequests(userId);
            return Request.CreateResponse(HttpStatusCode.OK, requests);
        }

        [HttpGet]
        public HttpResponseMessage AnyRequest(string userId, Guid groupId)
        {
            var requests = teacherService.AnyRequest(userId, groupId);
            return Request.CreateResponse(HttpStatusCode.OK, requests);
        }

        [HttpGet]
        public HttpResponseMessage RequestsCount(string userId)
        {
            var count = teacherService.RequestsCount(userId);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        [HttpPost]
        public HttpResponseMessage Response([FromBody]RequestViewModel viewModel, string userId)
        {
            viewModel.UidTo = userId;
            teacherService.Response(viewModel);
            return Request.CreateResponse(HttpStatusCode.OK, ModelState);
        }
    }
}
