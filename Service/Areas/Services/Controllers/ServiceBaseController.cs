using System.Web.Http;

namespace Service.Areas.Service.Controllers
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
