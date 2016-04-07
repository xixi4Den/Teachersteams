using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services;
using Teachersteams.Business.ViewModels;

namespace Teachersteams.Api.Controllers
{
    public class GroupController : ApiController
    {
        private readonly IGroupService groupService;

        public GroupController(IGroupService groupService)
        {
            this.groupService = groupService;
        }

        [HttpGet]
        public HttpResponseMessage GetForTeacher(string userId, GroupFilterType filterType, int pageIndex, int pageSize)
        {
            var groups = groupService.GetTeacherGroupTitles(userId, filterType, pageIndex, pageSize);
            return Request.CreateResponse(HttpStatusCode.OK, groups);
        }

        //// GET api/group/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/group
        [HttpPost]
        public HttpResponseMessage Post(string userId, [FromBody]AddGroupViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.OwnerId = userId;
                var group = groupService.CreateGroup(viewModel);
                return Request.CreateResponse(HttpStatusCode.Created, group);
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        //// PUT api/group/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/group/5
        //public void Delete(int id)
        //{
        //}
    }
}
