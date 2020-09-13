using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PetStore.Data.Repositorys
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<bool> Delete(TEntity entityToDelete);

        Task<bool> Delete(object id);

        Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetById(object id);

        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> Update(TEntity entityToUpdate);
    }
}