using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScribeServer.Models
{
    /// <summary>
    /// User is a data layer for the TestUsers that are hardcoded into the Authenication Configuration.
    /// </summary>
    public class User
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public UserRoles Role { get; set; }
    }

    /// <summary>
    /// Roles assigned to members who can create content.
    /// </summary>
    public enum UserRoles
    {
        Author,
        Admin
    }
}
