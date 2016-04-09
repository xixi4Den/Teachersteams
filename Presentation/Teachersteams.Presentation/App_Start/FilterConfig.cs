using System.Web.Mvc;
using Teachersteams.Presentation.Filters;

namespace Teachersteams.Presentation
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LocalizationAttribute());
        }
    }
}
