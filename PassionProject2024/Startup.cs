using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProject2024.Startup))]
namespace PassionProject2024
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
