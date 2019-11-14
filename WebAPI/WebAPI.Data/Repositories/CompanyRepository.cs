using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Contracts;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Data.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(DbContext dbContext) 
            : base(dbContext)
        { }
    }
}