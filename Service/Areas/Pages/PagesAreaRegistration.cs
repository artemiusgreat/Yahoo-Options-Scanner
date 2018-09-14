using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;

namespace IBService.Areas.View
{
  public class PagesAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "Pages";
      }
    }

    public void CreateRoutes(AreaRegistrationContext context)
    {
      context.MapRoute(
        name: "View",
        url: "",
        defaults: new { controller = "ViewBase", action = "Index" }
      );
    }

    public void CreatePackages(BundleCollection packages)
    {
      packages.Add(new ScriptBundle("~/Packages/Scripts").Include(
        "~/Areas/Pages/Application/code/inline.bundle.js",
        "~/Areas/Pages/Application/code/polyfills.bundle.js",
        "~/Areas/Pages/Application/code/main.bundle.js"
      ));

      packages.Add(new StyleBundle("~/Packages/Styles").Include(
        "~/Areas/Pages/Templates/Styles/FontAwesome.css",
        "~/Areas/Pages/Application/code/styles.bundle.css",
        "~/Areas/Pages/Templates/Styles/Template.css"
      ));
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {
      CreateRoutes(context);
      CreatePackages(BundleTable.Bundles);
    }
  }
}