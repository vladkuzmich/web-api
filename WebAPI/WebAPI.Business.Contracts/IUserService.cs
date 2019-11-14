using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Business.Contracts.Models;
using WebAPI.Business.Contracts.Models.Dtos;

namespace WebAPI.Business.Contracts
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);
        Task CreateAsync(UserDto userDto);
        Task EditAsync(int id, UserDto userDto);
        Task DeleteAsync(UserDto userDto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<IEnumerable<UserDto>> GetAllByCompanyAsync(int companyId);
        Task<OperationResult> ChangeCompanyAsync(int id, int companyId);
    }
}