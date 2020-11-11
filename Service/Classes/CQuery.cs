using System.Web;

namespace Service.Classes
{
  public class CQuery : IHttpModule
  {
    public void Init(HttpApplication context)
    {
      context.BeginRequest += (sender, args) =>
      {
        var app = (HttpApplication)sender;

        if (app.Request.HttpMethod.ToUpper().Equals("OPTIONS"))
        {
          app.Response.StatusCode = 200;
          app.Response.End();
        }
      };
    }

    public void Dispose()
    {
    }
  }
}