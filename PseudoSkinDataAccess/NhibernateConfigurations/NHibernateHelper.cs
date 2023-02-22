using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using PseudoSkinDataAccess.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.NhibernateConfigurations
{
    public class NHibernateHelper : INHibernateHelper
    {
        private ISessionFactory _sessionFactory;
        public NHibernateHelper(string connectionStrings)
        {
            if (_sessionFactory == null && !string.IsNullOrEmpty(connectionStrings))
            {
                _sessionFactory = Begin_ConfigurationSql(connectionStrings);
            }
        }

        private ISessionFactory Begin_ConfigurationSql(string connectionStrings)
        {
            return Fluently.Configure()
                           .Database(MsSqliteConfiguration.Standard.ConnectionString(connectionStrings))
                           .Mappings(x => x.FluentMappings.AddFromAssembly(typeof(PseudoskinMap).Assembly))
                           .ExposeConfiguration(cfg =>
                           {
                               new SchemaUpdate(cfg).Execute(false, true);
                               //cfg.SetProperty(NHibernate)
                           }).BuildSessionFactory();
        }

        public ISessionFactory SessionFactory { get => _sessionFactory; }
        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }

    public interface INHibernateHelper
    {
        ISession OpenSession();
    }
}
