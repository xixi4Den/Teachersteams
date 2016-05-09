using System.Web.Configuration;
using System.Web.Mvc;

namespace Teachersteams.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Settings()
        {
            return Json(new
            {
                BusinessApiUrl = WebConfigurationManager.AppSettings["BusinessApiUrl"]
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
