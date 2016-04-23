using System.Web.Mvc;

namespace Teachersteams.Presentation.Helpers
{
    public static class HtmlHelpers
    {
        public static string GetViewDataValue(this HtmlHelper helper, string key)
        {
            return helper.ViewData.ContainsKey(key) ? helper.ViewData[key].ToString() : string.Empty;
        }
    }
}