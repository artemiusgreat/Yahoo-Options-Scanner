using System.Web.Mvc;

namespace Service.Areas.View.Controllers
{
  public class ViewBaseController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }
  }
}
