using System;
using System.Diagnostics.Contracts;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Teachersteams.Business.Enums;
using Teachersteams.Business.Services;
using Teachersteams.Business.ViewModels.Group;

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

        [HttpGet]
        public HttpResponseMessage Get(Guid? id, string userId)
        {
            if (!id.HasValue)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "incorrect argument");
            }

            var groupInfo = groupService.GetGroupInfo(id.Value);
            return Request.CreateResponse(HttpStatusCode.OK, groupInfo);
        }

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
