using System;

namespace WebAPI.Business.Contracts.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public int CompanyId { get; set; }
    }
}
