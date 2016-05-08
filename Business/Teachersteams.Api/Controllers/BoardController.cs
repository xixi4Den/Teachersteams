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

        public BoardController(IStudentBoardItemsProvider studentBoardItemsProvider)
        {
            this.studentBoardItemsProvider = studentBoardItemsProvider;
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
            var items = studentBoardItemsProvider.AssignmentsCount(userId, filterType);
            return Request.CreateResponse(HttpStatusCode.OK, items);
        }
    }
}
