using System.Collections.Generic;
using System.Linq;
using WebAPI.Business.Contracts;
using WebAPI.Business.Contracts.Models.Dtos;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Business.Converters
{
    public class UserConverter : IUserConverter
    {
        public UserDto ToUserDto(User user) =>
            new UserDto
            {
                Name = user.Name,
                Surname = user.Surname,
                BirthDate = user.BirthDate,
                Email = user.Email,
                CompanyId = user.CompanyId
            };

        public IEnumerable<UserDto> ToUserDtos(IEnumerable<User> users) =>
            users.Select(ToUserDto);

        public User ToUser(UserDto userDto) =>
            new User
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Surname = userDto.Surname,
                BirthDate = userDto.BirthDate,
                Email = userDto.Email,
                CompanyId = userDto.CompanyId
            };

        public IEnumerable<User> ToUsers(IEnumerable<UserDto> userDtos) =>
            userDtos.Select(ToUser);
    }
}