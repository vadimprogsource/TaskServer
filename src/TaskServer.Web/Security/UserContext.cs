using System;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Repository;
using TaskServer.Interfaces.Security;

namespace TaskServer.Web.Security
{
    public class UserContext : IUserContext
    {

        public UserContext(IUserRepository context)
        {
            Logged = context.Login("Admin", "1");
        }

        public IAccount Logged
        {
            get;
           
        }

        public Guid Sid
        {
            get
            {
                return Logged.Id;
            }
        }
    }
}
