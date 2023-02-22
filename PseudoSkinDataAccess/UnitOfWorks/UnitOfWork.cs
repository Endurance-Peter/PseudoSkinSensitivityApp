using NHibernate;
using PseudoSkinDataAccess.NhibernateConfigurations;
using PseudoSkinDataAccess.Repositories;
using PseudoSkinDomain.Models;
using System;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransaction transaction;
        private ISession session;
        public UnitOfWork(INHibernateHelper nHibernateHelper)
        {
            session = nHibernateHelper.OpenSession();
            transaction = session.BeginTransaction();
            InitilizedRepository();
        }

        private void InitilizedRepository()
        {
            PseudoSkin = new Repository<PseudoSkin>(session);
            RegressionResult = new Repository<RegressionResult>(session);
        }
        public Repository<PseudoSkin> PseudoSkin { get ; set ; }
        public Repository<RegressionResult> RegressionResult { get ; set ; }

        public void Dispose()
        {
            session.Close();
            session = null;
        }

        public Task SaveChangesAsync()
        {
            try
            {
                if (transaction.IsActive) transaction.Commit();
            }
            catch (Exception)
            {

                if (transaction.IsActive) transaction.Rollback();
            }
            return Task.CompletedTask;
        }

        public void Rollback()
        {
            try
            {
                if (transaction.IsActive) transaction.Rollback();
            }
            finally
            {
                transaction.Rollback();
            }

        }
    }
}
