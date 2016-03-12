using FurrySystem;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace FurrySystem
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}