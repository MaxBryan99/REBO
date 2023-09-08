using Microsoft.Practices.Unity.WebApi;
using System.Web.Http;

namespace Bicimoto.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web
            config.DependencyResolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer());
            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}