using System.Collections.Generic;
using WebAPI.Business.Contracts.Models.Dtos;
using WebAPI.Data.Contracts.Entities;

namespace WebAPI.Business.Contracts
{
    public interface IUserConverter
    {
        UserDto ToUserDto(User user);
        IEnumerable<UserDto> ToUserDtos(IEnumerable<User> users);
        User ToUser(UserDto userDto);
        IEnumerable<User> ToUsers(IEnumerable<UserDto> userDtos);
    }
}