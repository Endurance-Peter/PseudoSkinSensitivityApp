using NHibernate;
using PseudoSkinDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PseudoSkinDataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T: BaseEntity
    {
        private readonly ISession _session;
        public Repository(ISession session)
        {
            _session = session;
        }
        public void Add(T model)
        {
            _session.Save(model);
        }

        public void Delete(Guid id)
        {
            var model = _session.Query<T>().FirstOrDefault(x => x.Id == id);
            _session.Delete(model);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            var models = _session.Query<T>().AsEnumerable<T>();

            return Task.FromResult(models);
        }

        public Task<T> GetById(Guid id)
        {
            var model = _session.Query<T>().FirstOrDefault(x => x.Id == id);

            return Task.FromResult(model);
        }

        public Task<T> GetByName(Expression<Func<T, bool>> predicate)
        {
            var pseudoskin = _session.Query<T>().FirstOrDefault(predicate);

            return Task.FromResult(pseudoskin);
        }

        public Task<bool> NameExist(Expression<Func<T,bool>> predicate)
        {
            var isNameExist = _session.Query<T>().Any(predicate);

            return Task.FromResult(isNameExist);
        }

        public void Update(T model)
        {
            _session.SaveOrUpdate(model);
        }
    }
}
