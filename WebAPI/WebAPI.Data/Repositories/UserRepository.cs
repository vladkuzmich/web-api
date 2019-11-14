using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Contracts;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext dbContext) 
            : base(dbContext)
        { }
    }
}