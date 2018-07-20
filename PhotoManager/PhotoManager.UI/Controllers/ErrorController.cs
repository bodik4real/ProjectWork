using System.Web.Mvc;

namespace PhotoManager.UI.Controllers
{
    public class ErrorController : Controller
    {
        public ViewResult NotFound()
        {
            return View();
        }
    }
}