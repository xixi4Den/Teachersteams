using System;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Teachersteams.Api.Filters
{
    public class ApiPermissionsAttribute : ActionFilterAttribute
    {
        private const string requestedNotFromVkErrorMessage = "Requested not from VK.";
        private const string missingUserIdParamErrorMessage = "Missing userId parameter.";
        private const string checkDigitalSignatureErrorMessage = "Digital signature check failed.";

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var referrer = GetReferrer(request);
            var viewerId = GetUserId(request);
            var expecteAuthKey = CalculateExpectedAuthKey(WebConfigurationManager.AppSettings["VkApiId"], 
                viewerId, WebConfigurationManager.AppSettings["VkSecureKey"]);
            var actualAuthKey = GetAuthKey(referrer);
            if (actualAuthKey != expecteAuthKey)
            {
                throw new HttpResponseException(request.CreateErrorResponse(
                    HttpStatusCode.Forbidden, checkDigitalSignatureErrorMessage));
            }

            base.OnActionExecuting(actionContext);
        }

        private static string CalculateExpectedAuthKey(string apiId, string viewerId, string secureKey)
        {
            var str = String.Concat(apiId, "_", viewerId, "_", secureKey);
            return CalculateMd5(str);
        }

        private static string GetAuthKey(Uri referrer)
        {
            return HttpUtility.ParseQueryString(referrer.Query).Get("auth_key");
        }

        private static Uri GetReferrer(HttpRequestMessage request)
        {
            var referrer = request.Headers.Referrer;

            if (referrer == null)
            {
                throw new HttpResponseException(request.CreateErrorResponse(
                    HttpStatusCode.Forbidden, requestedNotFromVkErrorMessage));
            }

            return referrer;
        }

        private static string GetUserId(HttpRequestMessage request)
        {
            var userId = HttpUtility.ParseQueryString(request.RequestUri.Query).Get("userId");

            if (userId == null)
            {
                throw new HttpResponseException(request.CreateErrorResponse(
                    HttpStatusCode.Forbidden, missingUserIdParamErrorMessage));
            }

            return userId;
        }

        private static string CalculateMd5(string str)
        {
            var encodedStr = new UTF8Encoding().GetBytes(str);
            var hash = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedStr);
            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }
    }
}