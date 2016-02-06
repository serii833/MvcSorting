using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IncomeViewer.Startup))]
namespace IncomeViewer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
//            ConfigureAuth(app);
        }
    }
}
