using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TodoSampleBackend.Startup))]

namespace TodoSampleBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}