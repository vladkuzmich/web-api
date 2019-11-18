using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Business.Contracts.Models.Dtos;

namespace WebAPI.Business.Contracts
{
    public interface ICompanyService
    {
        Task<CompanyDto> GetByIdAsync(int id);
        Task<IEnumerable<CompanyDto>> GetAllAsync();
        Task<CompanyDto> CreateAsync(CompanyDto companyDto);
        Task EditAsync(int id, CompanyDto companyDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersByCompanyIdAsync(int id);
    }
}