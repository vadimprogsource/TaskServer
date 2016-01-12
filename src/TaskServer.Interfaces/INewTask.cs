using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{


    //        ITask AddTask(string name , bool hasOwnControl, string tagret , IUser manager , PriorityCode  priorityCode ,  DateTime startDateTime , DateTime endDateTime);
    public interface INewTask
    {
        Guid? ParentTaskId { get; }
        Guid AutorId { get; }
        Guid EmployeeId { get; }


    }
}
