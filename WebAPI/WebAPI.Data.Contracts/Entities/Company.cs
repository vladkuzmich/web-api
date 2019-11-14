using System.Collections.Generic;

namespace WebAPI.Data.Contracts.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}