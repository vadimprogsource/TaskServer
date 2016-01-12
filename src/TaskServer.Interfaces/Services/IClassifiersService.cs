using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Services
{
    public interface IClassifiersService
    {
        IEnumerable<IStatus> GetStatuses();
        IEnumerable<IPriority> GetPriorities();

        IEnumerable<IUser> GetSystemUsers();


        IStatus    GetStatus(StatusCode code);
        IPriority  GetPriority(PriorityCode code);

    }
}
