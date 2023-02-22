using PseudoSkinDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.Repositories
{
    public interface IRepository<T> where T: BaseEntity
    {
        void Add(T model);
        void Update(T model);
        void Delete(Guid id);
        Task<T> GetById(Guid id);
        Task<T> GetByName(Expression<Func<T, bool>> predicate);
        Task<bool> NameExist(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
    }
}
