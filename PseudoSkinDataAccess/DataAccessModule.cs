using Prism.Ioc;
using Prism.Modularity;
using PseudoSkinDataAccess.Migrations;
using PseudoSkinDataAccess.NhibernateConfigurations;
using PseudoSkinDataAccess.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess
{
    public class DataAccessModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var connectionString = @"Data Source=PsuedoApp.db";
            var service = DataBaseRunner.CreateMigrations(connectionString);
            DataBaseRunner.RunMigrate(service);
            containerRegistry.RegisterScoped<IUnitOfWork, UnitOfWork>();
            containerRegistry.RegisterScoped<INHibernateHelper>(x => new NHibernateHelper(connectionString));
        }
    }
}
