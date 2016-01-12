using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{
   public interface IAccount : IUser
    {
        string LoginName { get; }
        Guid Password { get; }

        bool IsAdmin { get; }
        bool IsPowerUser { get; }
        bool IsUser { get; }
    }
}
