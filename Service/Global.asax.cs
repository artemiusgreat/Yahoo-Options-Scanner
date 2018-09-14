using IBService.Areas.View;
using MongoDB.Bson.Serialization;
using Service.Areas.Service;
using Service.Models.Data;
using System.Web.Mvc;
using System.Web.Routing;

namespace Service
{
  public class WebApiApplication : System.Web.HttpApplication
  {
    protected void Application_Start()
    {
      var serviceArea = new ServicesAreaRegistration();
      var serviceContext = new AreaRegistrationContext(serviceArea.AreaName, RouteTable.Routes);

      serviceArea.RegisterArea(serviceContext);

      var viewArea = new PagesAreaRegistration();
      var viewContext = new AreaRegistrationContext(viewArea.AreaName, RouteTable.Routes);

      viewArea.RegisterArea(viewContext);

      BsonClassMap.RegisterClassMap<CGroup>();
      BsonClassMap.RegisterClassMap<CQuote>();
      BsonClassMap.RegisterClassMap<COption>();
    }
  }
}
