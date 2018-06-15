using System.Web.Http;
using System.Web.Mvc;

namespace IBService.Areas.Service
{
  public class ServiceAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "Service";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      context.Routes.MapHttpRoute(
        name: "Service",
        routeTemplate: "",
        defaults: new { controller = "BaseService", action = "Index" }
      );

      context.Routes.MapHttpRoute(
        name: "Service.Options",
        routeTemplate: "service/options",
        defaults: new { controller = "ServiceOptions", action = "Options" }
      );

      context.Routes.MapHttpRoute(
        name: "Service.Options.Schedule",
        routeTemplate: "service/options/schedule",
        defaults: new { controller = "ServiceOptions", action = "ScheduleOptions" }
      );
    }
  }
}