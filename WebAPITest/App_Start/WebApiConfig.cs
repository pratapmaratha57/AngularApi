namespace WebAPITest
{
    using System.Web.Http;
    using System.Web.Http.ExceptionHandling;
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new CustomExceptionFilter());
            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
        }
    }
}
