using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Data.Contracts
{
    public interface IBaseRepository<TEntity>
        where TEntity: BaseEntity
    {
        void Create(TEntity entity);
        void Edit(TEntity entity);
        void Delete(int id);
        Task<IList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
    }
}