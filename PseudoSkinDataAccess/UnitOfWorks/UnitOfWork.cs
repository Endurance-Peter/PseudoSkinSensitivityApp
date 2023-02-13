using PseudoSkinDataAccess.Repositories;
using PseudoSkinDomain.Models;
using System;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private DbContextStore _dbContext;
        public UnitOfWork(DbContextStore dbContext)
        {
            _dbContext = dbContext;
            InitilizedRepository();
        }

        private void InitilizedRepository()
        {
            PseudoSkin = new Repository<PseudoSkin>(_dbContext);
            RegressionResult = new Repository<RegressionResult>(_dbContext);
        }
        public Repository<PseudoSkin> PseudoSkin { get ; set ; }
        public Repository<RegressionResult> RegressionResult { get ; set ; }

        public void Dispose()
        {
            _dbContext.DisposeAsync();
        }

        public Task SaveChangesAsync()
        {
            try
            {
                _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new ArgumentException(ex.Message);
            }


            return Task.CompletedTask;
        }
    }
}
