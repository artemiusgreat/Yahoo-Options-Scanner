using System.Web.Http;

namespace IBService.Areas.Service.Controllers
{
  public class BaseServiceController : ApiController
  {
    [AcceptVerbs("GET")]
    public string Index()
    {
      return null;
    }
  }
}
