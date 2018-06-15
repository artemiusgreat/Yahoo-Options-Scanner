using IBService.Areas.Service;
using System.Web.Mvc;
using System.Web.Routing;

namespace IBService
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      var serviceArea = new ServiceAreaRegistration();
      var serviceContext = new AreaRegistrationContext(serviceArea.AreaName, RouteTable.Routes);

      serviceArea.RegisterArea(serviceContext);
    }
  }
}
