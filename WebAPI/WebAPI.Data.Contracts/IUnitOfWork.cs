using System.Threading.Tasks;

namespace WebAPI.Data.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        ICompanyRepository Companies { get; }
        Task<int> CommitAsync();
    }
}