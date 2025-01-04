using System.Collections.Generic;

namespace PRDenaCo.Domain.Entities.Users
{
    public class Role
    {
        public int Id { get; set; }
        public string  Name { get; set; }
        public ICollection<UserInRole > UserInRoles { get; set; }
    }
}
