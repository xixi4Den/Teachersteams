using System.Web.Mvc;

namespace Teachersteams.Presentation.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InternalError()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
	}
}