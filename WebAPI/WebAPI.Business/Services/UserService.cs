using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using WebAPI.Business.Contracts;
using WebAPI.Business.Contracts.Models.Dtos;
using WebAPI.Data.Contracts;

namespace WebAPI.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserConverter _userConverter;
        private readonly IImageProvider _imageProvider;

        public UserService(
            IUnitOfWork uow, 
            ILogger<UserService> logger,
            IImageProvider imageProvider,
            IUserConverter userConverter)
            : base(uow, logger)
        {
            _imageProvider = imageProvider ?? throw new ArgumentNullException(nameof(imageProvider));
            _userConverter = userConverter ?? throw new ArgumentNullException(nameof(userConverter));
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await Uow.Users.GetByIdAsync(id);

            return user == null
                ? null
                : _userConverter.ToUserDto(user);
        }

        public async Task<UserDto> CreateAsync(UserDto userDto)
        {
            ThrowIfNull(userDto);

            try
            {
                var user = _userConverter.ToUser(userDto);
                Uow.Users.Create(user);
                await Uow.CommitAsync();

                return _userConverter.ToUserDto(user);
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

            var user = await Uow.Users.GetByIdAsync(userDto.Id);
            if (user == null)
            {
                throw new ArgumentException($"User with id: '{id}' not found", nameof(id));
            }

            try
            {
                user.Name = userDto.Name;
                user.Surname = userDto.Surname;
                user.Email = userDto.Email;
                user.BirthDate = userDto.BirthDate;

                Uow.Users.Edit(user);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e,$"Error while editing user with id: '{userDto.Id}'");
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await Uow.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"User with id: '{id}' not found", nameof(id));
            }

            try
            {
                Uow.Users.Delete(user);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while deleting user with id: '{id}'");
                throw;
            }
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync() =>
            _userConverter.ToUserDtos(await Uow.Users.GetAllAsync());

        public async Task ChangeCompanyAsync(int userId, int companyId)
        {
            var user = await Uow.Users.GetByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException($"User with id: '{userId}' not found", nameof(userId));
            }

            var company = await Uow.Companies.GetByIdAsync(companyId);
            if (company == null)
            {
                throw new ArgumentException($"Company with id: '{companyId}' not found", nameof(companyId));
            }

            try
            {
                user.CompanyId = companyId;
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while changing user's company with UserId: '{userId}' and CompanyId: '{companyId}'");
                throw;
            }
        }

        public async Task UploadPhotoAsync(int id, string fileName, byte[] imageData, string storagePath, int width, int height)
        {
            var user = await Uow.Users.GetByIdAsync(id);
            if (user == null)
            {
                throw new ArgumentException($"User with id: '{id}' not found", nameof(id));
            }

            if (storagePath == null)
            {
                throw new ArgumentException(nameof(storagePath));
            }

            try
            {
                var newFileName = $"{id.ToString()}_{fileName}";
                var fullImageUrl = Path.Combine(storagePath, newFileName);

                _imageProvider.UploadImage(imageData, fullImageUrl, width, height);

                user.ImageUrl = Path.GetRelativePath(newFileName, fullImageUrl);
                Uow.Users.Edit(user);
                await Uow.CommitAsync();
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Error while uploading user's photo with UserId: '{id}'");
                throw;
            }
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