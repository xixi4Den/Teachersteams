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
    public class TeacherController : ApiController
    {
        private readonly IUserService userService;

        public TeacherController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public HttpResponseMessage Post(string userId, [FromBody]TeacherViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var teacher = userService.InviteTeacher(viewModel);
                return Request.CreateResponse(HttpStatusCode.Created, teacher);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //[Route("api/teacher/{groupId}/list")]
        [HttpGet]
        public HttpResponseMessage Get(Guid groupId, string userId, [FromUri]GridOptions options)
        {
            var teachers = userService.GetTeachers(groupId, options);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }

        //[Route("api/teacher/{groupId}/count")]
        [HttpGet]
        public HttpResponseMessage Count(Guid groupId, string userId)
        {
            var teachers = userService.Count(groupId);
            return Request.CreateResponse(HttpStatusCode.OK, teachers);
        }
    }
}
