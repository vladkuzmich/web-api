using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Business.Contracts.Dtos;

namespace WebAPI.Business.Contracts
{
    public interface ICompanyService
    {
        Task GetByIdAsync(int id);
        Task CreateAsync(CompanyDto companyDto);
        Task EditAsync(CompanyDto companyDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<CompanyDto>> GetAllAsync();
    }
}