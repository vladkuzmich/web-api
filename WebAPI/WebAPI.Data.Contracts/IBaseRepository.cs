using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Data.Contracts
{
    public interface IBaseRepository<TEntity>
        where TEntity: BaseEntity
    {
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Delete(TEntity entity);
        Task<IList<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
    }
}