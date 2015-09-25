using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOA_Assignment_2.Startup))]
namespace SOA_Assignment_2
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
