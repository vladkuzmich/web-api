using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebAPI.Business.Contracts;
using WebAPI.Business.Contracts.Models;
using WebAPI.Business.Contracts.Models.Dtos;
using WebAPI.Data.Contracts;

namespace WebAPI.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserConverter _userConverter;

        public UserService(
            IUnitOfWork uow, 
            ILogger<UserService> logger, 
            IUserConverter userConverter)
            : base(uow, logger)
        {
            _userConverter = userConverter ?? throw new ArgumentNullException(nameof(userConverter));
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await Uow.Users.GetByIdAsync(id);

            return user == null
                ? null
                : _userConverter.ToUserDto(user);
        }

        public async Task CreateAsync(UserDto userDto)
        {
            ThrowIfNull(userDto);

            try
            {
                Uow.Users.Create(_userConverter.ToUser(userDto));

                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error while creating user");
                throw;
            }
        }

        public async Task EditAsync(int id, UserDto userDto)
        {
            ThrowIfNull(userDto);

            try
            {
                userDto.Id = id;

                Uow.Users.Edit(_userConverter.ToUser(userDto));

                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e,$"Error while editing user with id: {id}");
                throw;
            }
        }

        public async Task DeleteAsync(UserDto userDto)
        {
            ThrowIfNull(userDto);

            try
            {
                Uow.Users.Delete(_userConverter.ToUser(userDto));

                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while deleting user with id: {userDto.Id}");
                throw;
            }
        }

        public Task<IEnumerable<UserDto>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetAllByCompanyAsync(int companyId)
        {
            throw new System.NotImplementedException();
        }

        public Task<OperationResult> ChangeCompanyAsync(int id, int companyId)
        {
            throw new System.NotImplementedException();
        }

        private void ThrowIfNull(UserDto userDto)
        {
            if (userDto == null)
            {
                throw new ArgumentNullException(nameof(userDto));
            }
        }
    }
}