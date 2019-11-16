using System.Collections.Generic;
using System.Linq;
using WebAPI.API.Converters.Interfaces;
using WebAPI.API.Models.Documents;
using WebAPI.Business.Contracts.Models.Dtos;

namespace WebAPI.API.Converters
{
    public class UserDocumentConverter : IUserDocumentConverter
    {
        public UserDocument ToUserDocument(UserDto userDto) =>
            new UserDocument
            {
                Id = userDto.Id,
                Name = userDto.Name,
                Surname = userDto.Surname,
                Email = userDto.Email,
                BirthDate = userDto.BirthDate
            };

        public IEnumerable<UserDocument> ToUserDocuments(IEnumerable<UserDto> userDtos) =>
            userDtos.Select(ToUserDocument);

        public UserDto ToUserDto(UserDocument userDocument) =>
            new UserDto
            {
                Id = userDocument.Id,
                Name = userDocument.Name,
                Surname = userDocument.Surname,
                Email = userDocument.Email,
                BirthDate = userDocument.BirthDate
            };

        public IEnumerable<UserDto> ToUserDtos(IEnumerable<UserDocument> userDocuments) =>
            userDocuments.Select(ToUserDto);
    }
}
