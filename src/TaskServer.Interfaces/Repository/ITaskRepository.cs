using TaskServer.Interfaces.Filtration;
using System;
using System.Collections.Generic;
using TaskServer.Interfaces.Specialized;

namespace TaskServer.Interfaces.Repository
{
    public interface ITaskRepository
    {
        IEnumerable<IPriority> GetTaskPriorities();
        IEnumerable<IStatus> GetTaskStatuses();

        ITask GetTask(Guid taskId);
        ITask InsertTask(IDelta<ITask> task);
        ITask UpdateTask(IDelta<ITask> task);
        void  RemoveTask(Guid taskId);
        ITask GetTaskWithChilds(Guid taskId);
        IEnumerable<ITask> GetTasksByEmployee(int maxCount,Guid employeeId,params StatusCode[] statuses);
        ICompositeTaskSet ApplyTaskFilterWithCompute(Guid? ownerId, ITaskFilter filter);
        IPage<ITask>      ApplyTaskFilterOnly(Guid? ownerId, ITaskFilter filter);

    }
}
