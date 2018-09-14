using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;

namespace Service.Areas.Service
{
  public class ServicesAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "Services";
      }
    }

    public void CreateRoutes(AreaRegistrationContext context)
    {
      context.Routes.MapHttpRoute(
        name: "Services.Options.Scan",
        routeTemplate: "service/options/scan",
        defaults: new { controller = "ServiceOptions", action = "ScanOptions" }
      );

      context.Routes.MapHttpRoute(
        name: "Services.Options.Load",
        routeTemplate: "service/options/load",
        defaults: new { controller = "ServiceOptions", action = "LoadOptions" }
      );
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      CreateRoutes(context);
    }
  }
}