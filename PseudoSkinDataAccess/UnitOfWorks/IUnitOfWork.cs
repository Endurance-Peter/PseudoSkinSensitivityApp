using PseudoSkinDataAccess.Repositories;
using PseudoSkinDomain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        public Repository<PseudoSkin> PseudoSkin { get; set; }
        public Repository<RegressionResult> RegressionResult { get; set; }
        public Task SaveChangesAsync();
    }
}
