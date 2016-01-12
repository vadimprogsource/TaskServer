using TaskServer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces.Specialized;
using TaskServer.Interfaces.Filtration;

namespace TaskServer.Interfaces.Services
{
    public interface ITaskWorkflowService
    {
        IDelta<ITask> CreateTask();
        IDelta<ITask> GetTask(Guid id);
        IDelta<ITask> AddTask(ITask task);
        IDelta<ITask> StartTask(ITask task);
        IDelta<ITask> EndingTask(ITask task);
        IDelta<ITask> AcceptTask(ITask task);
        IDelta<ITask> RevertToRevision(ITask task);
        IDelta<ITask> RejectTask(ITask task);
        IDelta<ITask> UpdateTask(ITask task);
        IDelta<ITask> RemoveTask(ITask task);
        IEnumerable<ITask> GetMyTasks();
        IPage<ITask> GetTasksByFilter(ITaskFilter filter);
        ICompositeTaskSet GetCompositeTasks(ITaskFilter filter);   
    }
}
