using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StorageProject.Startup))]
namespace StorageProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
