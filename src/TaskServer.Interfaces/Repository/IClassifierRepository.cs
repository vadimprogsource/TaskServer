using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Repository
{
    public interface IClassifierRepository
    {
        IEnumerable<IPriority> GetTaskPriorities();
        IEnumerable<IStatus> GetTaskStatuses();
        IStatus GetStatus(int id);
        IPriority GetPriority(int id);
    }
}
