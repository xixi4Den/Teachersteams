using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace Teachersteams.Presentation.Filters
{
    public class LocalizationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var language = request.Params.Get("language");

            if (!SetCulture(language))
            {
                var referer = request.UrlReferrer;
                if (referer != null)
                {
                    language = HttpUtility.ParseQueryString(referer.Query).Get("language");
                    SetCulture(language);
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private static bool SetCulture(string language)
        {
            if (language != null)
            {
                if (language == "3")
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
                }
                else
                {
                    Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("ru-RU");
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("ru-RU");
                }
                return true;
            }
            return false;
        }
    }
}