using Microsoft.Extensions.DependencyInjection;
using PseudoSkinClient;
using PseudoSkinClient.ChartServices;
using PseudoSkinDataAccess;
using PseudoSkinDataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Windows;

namespace PseudoSkinServices
{
    public class RegisterServices
    {
        public static ServiceProvider RegisterAllServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<DbContextStore>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAllService, Service>();
            services.AddScoped<ChatView>();

            return services.BuildServiceProvider();
        }
        public static void ShowView(IServiceProvider serviceProvider)
        {
            serviceProvider.GetService<ChatView>()?.Show();
        }
    }
        
}
