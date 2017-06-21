using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Web.Http;
using Autofac.Integration.WebApi;
using TodoSampleBackend.Data;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using Owin;

namespace TodoSampleBackend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            config.MapHttpAttributeRoutes();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Automatic Code First Migrations
            var migrator = new DbMigrator(new Migrations.Configuration());
            migrator.Update();

            Bootstrapper.Run();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(AppAutoFac.Container);
            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            var settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();
            app.UseAutofacMiddleware(AppAutoFac.Container);

            if (string.IsNullOrEmpty(settings.HostName))
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] {ConfigurationManager.AppSettings["ValidAudience"]},
                    ValidIssuers = new[] {ConfigurationManager.AppSettings["ValidIssuer"]},
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            app.UseWebApi(config);
        }
    }
}