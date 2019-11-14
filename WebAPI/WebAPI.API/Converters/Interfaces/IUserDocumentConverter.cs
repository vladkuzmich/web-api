using System.Collections.Generic;
using WebAPI.API.Documents;
using WebAPI.Business.Contracts.Models.Dtos;

namespace WebAPI.API.Converters.Interfaces
{
    public interface IUserDocumentConverter
    {
        UserDocument ToUserDocument(UserDto userDto);
        IEnumerable<UserDocument> ToUserDocuments(IEnumerable<UserDto> userDtos);
        UserDto ToUserDto(UserDocument userDocument);
        IEnumerable<UserDto> ToUserDtos(IEnumerable<UserDocument> userDocuments);
    }
}
