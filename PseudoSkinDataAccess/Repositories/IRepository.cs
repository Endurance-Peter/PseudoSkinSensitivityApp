using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.Repositories
{
    public interface IRepository<T> where T:class
    {
        Task Add(T model);
        Task Delete(T model);
        Task Update(T model);
        Task<T> FetchById(Guid id);
        Task<List<T>> FetchAll();
    }
}
