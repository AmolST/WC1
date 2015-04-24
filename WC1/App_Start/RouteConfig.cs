using System.Web.Mvc;
using System.Web.Routing;

namespace WC1
{
  public class RouteConfig
  {
    public static void RegisterRoutes(RouteCollection routes)
    {
      routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

      routes.MapRoute(
          name: "Default",
          url: "{controller}/{action}/{id}",
          defaults: new { controller = "Fixture", action = "Index", id = UrlParameter.Optional },
          namespaces: new[] { "WC1.Controllers" }
      );
    }
  }
}