using System.Web.Http;

namespace IBService.Areas.Service.Controllers
{
  public class ServiceBaseController : ApiController
  {
    [AcceptVerbs("GET")]
    public string Index()
    {
      return null;
    }
  }
}
