using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teachersteams.Business.Services;
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
    }
}
