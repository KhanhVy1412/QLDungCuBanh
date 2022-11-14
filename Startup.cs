using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QLDungCuBanh.Startup))]
namespace QLDungCuBanh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
