using Backend.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Backend.Areas.Identity.IdentityHostingStartup))]
namespace Backend.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                // services.AddDefaultIdentity<IdentityUser>()
                //     .AddRoles<IdentityRole>()
                //     .AddEntityFrameworkStores<ApplicationDbContext>();
            });
        }
    }
}
