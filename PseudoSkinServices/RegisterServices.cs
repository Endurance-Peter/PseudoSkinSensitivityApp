using Microsoft.Extensions.DependencyInjection;
using PseudoSkinDataAccess;
using PseudoSkinDataAccess.UnitOfWorks;
using System;

namespace PseudoSkinServices
{
    public class RegisterServices
    {
        public RegisterServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DbContextStore>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
