using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Swashbuckle.Application;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(FordAPI.Startup))]

namespace FordAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public void Configuration(IAppBuilder app)
        {
            //configuração WebApi
            var config = new HttpConfiguration();

            config.Formatters.JsonFormatter
                        .SerializerSettings
                        .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //var politicas = new EnableCorsAttribute(
            //origins: "*",
            //methods: "*",
            //headers: "*"
            //);
            //config.EnableCors(politicas);

            //app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();

            //configurando rotas
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                 name: "DefaultApi",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { id = RouteParameter.Optional }
             );
            config.EnableSwagger(c =>
            {
                c.SingleApiVersion("v1", "FordAPI");
                c.ResolveConflictingActions(x => x.First());
                c.RootUrl(req => req.RequestUri.GetLeftPart(UriPartial.Authority)
                + VirtualPathUtility.ToAbsolute("~/").Trim('/'));
            }).EnableSwaggerUi();

            //token
            ConfgureAccessToken(app);

            //ativando o cors
            app.UseCors(CorsOptions.AllowAll);

            // ativando configuração WebApi
            app.UseWebApi(config);

        }


        private void ConfgureAccessToken(IAppBuilder app)
        {
            var optionsConfigurationToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new ProviderTokenAccess()
            };

            app.UseOAuthAuthorizationServer(optionsConfigurationToken);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
