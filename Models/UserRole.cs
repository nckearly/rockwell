using System;
using System.Collections.Generic;

namespace Rockwell.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Contact = new HashSet<Contact>();
        }

        public int UserRolePk { get; set; }
        public string UserRoleName { get; set; }
        public string UserRoleFunction { get; set; }

        public virtual ICollection<Contact> Contact { get; set; }
    }
}
