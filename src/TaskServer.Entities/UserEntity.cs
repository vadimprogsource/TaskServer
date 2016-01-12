using System;
using TaskServer.Interfaces;

namespace TaskServer.Entities
{

    public enum UserRole : int {Inactive = 0, User = 100 , PowerUser = 200 , Admin = 300 }

    public class UserEntity : NamedEntity,IAccount
    {
        public int TotalTasks { get; set; }

        public bool IsAdmin
        {
            get
            {
                return Role >= UserRole.Admin;
            }
        }

        public bool IsPowerUser
        {
            get
            {
                return Role >= UserRole.PowerUser && Role < UserRole.Admin;
            }
        }

        public bool IsUser
        {
            get
            {
                return Role >= UserRole.User && Role < UserRole.PowerUser;
            }
        }

        public UserRole Role { get; set; }

        public string LoginName { get; set; }
        public Guid Password { get; set; }
        
    }
}
