using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Business.Contracts.Models.Dtos;

namespace WebAPI.Business.Contracts
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(UserDto userDto);
        Task EditAsync(int id, UserDto userDto);
        Task DeleteAsync(int id);
        Task ChangeCompanyAsync(int userId, int companyId);
    }
}