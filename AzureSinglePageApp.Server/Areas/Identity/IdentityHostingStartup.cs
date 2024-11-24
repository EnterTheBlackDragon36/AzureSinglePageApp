[assembly: HostingStartup(typeof(AzureSinglePageApp.Server.Areas.Identity.IdentityHostingStartup))]
namespace AzureSinglePageApp.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
