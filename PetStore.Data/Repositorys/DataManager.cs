using Microsoft.EntityFrameworkCore;
using PetStore.Domain.Models;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PetStore.Data.Repositorys
{
    /// <summary>
    /// Generic Data Manager Class
    /// Uses an Entity Framework Data Context to do CRUD operations
    /// on ANY entity.
    /// Customize to your own liking.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class DataManager<TEntity, TDataContext> : IRepository<TEntity>
        where TEntity : BaseModel where TDataContext : DbContext
    {
        protected readonly TDataContext context;
        internal DbSet<TEntity> dbSet;

        protected IAsyncPolicy _retryPolicy;

        public DataManager(TDataContext dataContext, IAsyncPolicy retryPolicy)
        {
            context = dataContext;
            dbSet = context.Set<TEntity>();
            _retryPolicy = retryPolicy;
        }

        /// <summary>
        /// Generic Get lets you specify a LINQ filter.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="MyUserId"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                // Get the dbSet from the Entity passed in
                IQueryable<TEntity> query = dbSet;

                // Apply the filter
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                // Include the specified properties
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                // Sort
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                return null;
            }
        }

        // Get by primary key
        public virtual async Task<TEntity> GetById(object id)
        {
            return await dbSet.FindAsync(id);
        }

        // Insert an entity
        public virtual async Task<TEntity> Insert(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        // Delete by primary key
        public virtual async Task<bool> Delete(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            return await Delete(entityToDelete);
        }

        // Delete by entity
        public virtual async Task<bool> Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return await context.SaveChangesAsync() >= 1;
        }

        // Update
        public virtual async Task<TEntity> Update(TEntity entityToUpdate)
        {
            var dbSet = context.Set<TEntity>();
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entityToUpdate;
        }
    }
}