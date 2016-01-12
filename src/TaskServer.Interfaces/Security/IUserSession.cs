using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Security
{
    public interface IUserSession
    {
         Guid Sid { get; }
        IUser User { get; }
    }
}
