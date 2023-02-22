using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.Migrations
{
    public static class DataBaseRunner
    {
        public static IServiceProvider CreateMigrations(string connectionStrings)
        {
            return new ServiceCollection().AddFluentMigratorCore()
                                          .ConfigureRunner(x => x.AddSQLite().WithGlobalConnectionString(connectionStrings).ScanIn(typeof(DataBaseRunner).Assembly).For.Migrations())
                                          .BuildServiceProvider();
        }
        public static void RunMigrate(IServiceProvider service)
        {
            //var service = (IServiceProvider)Activator.CreateInstance(typeof(IServiceProvider));
            using (var scope = service.CreateScope())
            {
                var run = scope.ServiceProvider.GetService<IMigrationRunner>();
                run.MigrateUp();
            }
        }
    }
}
