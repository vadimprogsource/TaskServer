using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Repository
{
    public interface IUserRepository
    {
        IEnumerable<IUser> GetUsers();
        IAccount Login(string userName, string password);
        void UpdateUser(IUser user);
        IAccount RegisterUser(string userName, string password);
        IAccount SetPassword(Guid userId, string newPassword);
        void RemoveUser(Guid userId);
    }
}
