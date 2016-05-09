using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services.Contracts;
using Teachersteams.Business.ViewModels.Grid;

namespace Teachersteams.Api.Controllers
{
    public class BoardController : ApiController
    {
        private readonly IStudentBoardItemsProvider studentBoardItemsProvider;
        private readonly ITeacherBoardItemsProvider teacherBoardItemsProvider;

        public BoardController(IStudentBoardItemsProvider studentBoardItemsProvider,
            ITeacherBoardItemsProvider teacherBoardItemsProvider)
        {
            this.studentBoardItemsProvider = studentBoardItemsProvider;
            this.teacherBoardItemsProvider = teacherBoardItemsProvider;
        }

        [HttpGet]
        public HttpResponseMessage GetForStudent(string userId, StudentBoardFilterType filterType, [FromUri]GridOptions options)
        {
            var items = studentBoardItemsProvider.GetAssignments(userId, filterType, options);
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [HttpGet]
        public HttpResponseMessage CountForStudent(string userId, StudentBoardFilterType filterType)
        {
            var count = studentBoardItemsProvider.AssignmentsCount(userId, filterType);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }

        [HttpGet]
        public HttpResponseMessage GetForTeacher(string userId, [FromUri]GridOptions options, TeacherBoardAssignFilterType assignFilterType, TeacherBoardCheckFilterType? checkFilterType = null)
        {
            var items = teacherBoardItemsProvider.GetAssignments(userId, checkFilterType, assignFilterType, options);
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }

        [HttpGet]
        public HttpResponseMessage CountForTeacher(string userId, TeacherBoardAssignFilterType assignFilterType, TeacherBoardCheckFilterType? checkFilterType = null)
        {
            var count = teacherBoardItemsProvider.AssignmentsCount(userId, checkFilterType, assignFilterType);
            return Request.CreateResponse(HttpStatusCode.OK, count);
        }
    }
}
