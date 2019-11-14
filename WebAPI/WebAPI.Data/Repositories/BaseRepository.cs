using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Contracts;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity: BaseEntity, new()
    {
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            DbSet = dbContext.Set<TEntity>();
        }

        public virtual void Create(TEntity entity)
        {
            ThrowIfNull(entity);

            DbSet.Add(entity);
        }

        public virtual void Edit(TEntity entity)
        {
            ThrowIfNull(entity);

            DbSet.Update(entity);
        }

        public virtual void Delete(TEntity entity) => 
            DbSet.Remove(entity);

        public virtual async Task<IList<TEntity>> GetAllAsync() => 
            await DbSet.ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(int id) => 
            await DbSet.FindAsync(id);

        private void ThrowIfNull(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
        }
    }
}