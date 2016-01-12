using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Security
{
    public interface IUserContext
    {
        Guid Sid        { get; }
        IAccount Logged { get;}

   
    }
}
