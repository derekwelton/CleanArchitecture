using System;
using Application.Common.Installers;
using Application.Repositories.Contact;
using Infrastructure.Repositories.SQL;
using Infrastructure.Repositories.SQL.Contact;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var dbType = configuration["DBType"];
            switch (dbType)
            {
                case "SQL":
                    InstallSQLServices(services);
                    break;
                case "MySQL":

                    break;
                case "InMemory":

                    break;
            }
        }


        private void InstallSQLServices(IServiceCollection services)
        {
            services.AddTransient<SQLService>();
            services.AddTransient<IContactRepository, ContactSQLService>();
        }

        private void InstallMySQLServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
        private void InstallInMemoryServices(IServiceCollection services)
        {
            throw new NotImplementedException();
        }
    }
}