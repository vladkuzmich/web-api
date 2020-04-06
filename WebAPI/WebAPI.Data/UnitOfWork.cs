using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Contracts;
using WebAPI.Data.Repositories;

namespace WebAPI.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _dbContext;

        private IUserRepository _userRepository;
        private ICompanyRepository _companyRepository;
        
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IUserRepository Users => _userRepository ??= new UserRepository(_dbContext);
        public ICompanyRepository Companies => _companyRepository ??= new CompanyRepository(_dbContext);

        public Task<int> CommitAsync() => _dbContext.SaveChangesAsync();

        public void Dispose() => _dbContext?.Dispose();
    }
}