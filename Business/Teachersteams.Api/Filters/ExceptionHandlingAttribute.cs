using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Teachersteams.Business.Exceptions;

namespace Teachersteams.Api.Filters
{
    public class ExceptionHandlingAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var request = context.ActionContext.Request;
            if (context.Exception is BusinessException)
            {
                context.Response = request.CreateErrorResponse(HttpStatusCode.BadRequest, context.Exception.Message);
            }
            else
            {
                context.Response = request.CreateErrorResponse(HttpStatusCode.InternalServerError,
                    "Your request cannot be processed. Please try again or contact the support service.");
            }
            base.OnException(context);
        }
    }
}