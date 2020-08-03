using Application.Common.Identity;
using Application.Common.Installers;
using Infrastructure.Common.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            // Add identity types
            services.AddIdentity<IApplicationUser, IApplicationRole>()
                .AddDefaultTokenProviders();

            // Identity Services
            services.AddTransient<IUserStore<IApplicationUser>, ApplicationUserStore>();
            services.AddTransient<IRoleStore<IApplicationRole>, ApplicationRoleStore>();
        }
    }
}