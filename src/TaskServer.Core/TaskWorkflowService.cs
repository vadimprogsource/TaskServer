using System;
using System.Collections.Generic;
using System.Linq;
using TaskServer.Core.Helpers;
using TaskServer.Core.Specialized;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Filtration;
using TaskServer.Interfaces.Repository;
using TaskServer.Interfaces.Security;
using TaskServer.Interfaces.Services;
using TaskServer.Interfaces.Specialized;
using TaskServer.Objects;

namespace TaskServer.Core
{

    public class TaskWorkflowService : ITaskWorkflowService
    {

        private IUserContext        userContext;
        private ITaskRepository     repositoryContext;
        private IClassifiersService clsService;


        public TaskWorkflowService(IUserContext userContext , ITaskRepository repositoryContext,IClassifiersService clsService)
        {
            this.userContext       = userContext;
            this.repositoryContext = repositoryContext;
            this.clsService        = clsService;
        }




        public IDelta<ITask> CreateTask()
        {

            TaskObject task = new TaskObject();

            task.Manager = new UserObject(userContext.Logged);

            task.ToEmployee = null;

            task.Priority = new TaskPriority(clsService.GetPriority(PriorityCode.Medium));


            task.StartDateTime = task.CreatedDateTime;
            task.EndDateTime   = task.CreatedDateTime.AddDays(1);


            return new Delta<ITask>(task).AsNewTask();
             
        }

        public IDelta<ITask> AddTask(ITask task)
        {

            TaskObject taskPtr;

            IDelta<ITask> result = new Delta<ITask>(taskPtr = new TaskObject(task));


            if (string.IsNullOrWhiteSpace(task.Objective))
            {
                result.ApplyThrowError(x => x.Objective);
            }

            if (string.IsNullOrWhiteSpace(task.Name))
            {
                result.ApplyThrowError(x => x.Name);
            }

            if (task.StartDateTime > task.EndDateTime || task.StartDateTime ==DateTime.MinValue || task.EndDateTime == DateTime.MinValue)
            {
                result.ApplyThrowError(x => x.StartDateTime)
                      .ApplyThrowError(x => x.EndDateTime  );
            }


            if (result.HasErrors)
            {
                return result.AsNewTask();
            }



            taskPtr.Id = Guid.NewGuid();

            taskPtr.CreatedDateTime = DateTime.Now;
            taskPtr.Author          = new UserObject(userContext.Logged);

            taskPtr.Status = new TaskStatus(clsService.GetStatus(StatusCode.Pending));


            if (task.ToEmployee == null)
            {
                taskPtr.ToEmployee = taskPtr.Author;
            }

            if (task.Manager==null)
            {
                taskPtr.Manager = taskPtr.Author;
            }



            result
                .AcceptUpdate(x=>x.Id)
                .AcceptUpdate(x=>x.Status)
                .AcceptUpdate(x=>x.Priority)
                .AcceptUpdate(x => x.Name)
                .AcceptUpdate(x=>x.Objective)
                .AcceptUpdate(x=>x.Author)
                .AcceptUpdate(x=>x.Manager)
                .AcceptUpdate(x=>x.ToEmployee)
                .AcceptUpdate(x=>x.CreatedDateTime)
                .AcceptUpdate(x=>x.StartDateTime)
                .AcceptUpdate(x => x.EndDateTime);

            return repositoryContext
                   .InsertTask(result)
                   .ToDelta   ();
        }





        public IDelta<ITask> StartTask(ITask task)
        {

            if (userContext.IsEqual(task.ToEmployee) && task.Status.Code == StatusCode.Pending)
            {
                return repositoryContext.UpdateTask(task.ChangeStatus(StatusCode.InProcess)).ToDelta();
            }

            return task.ToDelta();
        }



        public IDelta<ITask> EndingTask(ITask task)
        {

            if (userContext.IsEqual(task.ToEmployee) && task.Status.Code == StatusCode.InProcess)
            {
                return repositoryContext.UpdateTask(task.ChangeStatus(StatusCode.Done)).ToDelta();
            }

            return task.ToDelta();
        }


        public IDelta<ITask> AcceptTask(ITask task)
        {
            if (userContext.IsEqual(task.Manager) && task.Status.Code == StatusCode.Done)
            {
                return repositoryContext.UpdateTask(task.ChangeStatus(StatusCode.Accepted)).ToDelta();
            }

            return task.ToDelta();
        }


        public IDelta<ITask> RejectTask(ITask task)
        {

            if (userContext.IsEqual(task.Author))
            {
                return repositoryContext.UpdateTask(task.ChangeStatus(StatusCode.Refused)).ToDelta();
            }

            return task.ToDelta();
        }



        public IDelta<ITask> RevertToRevision(ITask task)
        {

            if (userContext.IsEqualOr(task.Manager, task.Author) && task.Status.Code == StatusCode.Done)
            {
                return repositoryContext.UpdateTask(task.ChangeStatus(StatusCode.Revision)).ToDelta();
            }

            return task.ToDelta();
        }


        public IDelta<ITask> RemoveTask(ITask task)
        {

            if (userContext.IsEqual(task.Author))
            {
                repositoryContext.RemoveTask(task.Id);
            }


            return task.ToDelta();

        }

    

      

        public IDelta<ITask> UpdateTask(ITask task)
        {
            IDelta<ITask> taskResult = task.ToDelta();

            if (userContext.IsEqualOr(task.Author, task.Manager))
            {
                taskResult.AcceptUpdate(x => x.Priority)
                          .AcceptUpdate(x => x.Description);
            }

            if (userContext.IsEqual(task.Author) && task.Status.Code == StatusCode.Pending)
            {
                taskResult
                     .AcceptUpdate(x => x.Name)
                     .AcceptUpdate(x => x.Objective)
                     .AcceptUpdate(x => x.StartDateTime)
                     .AcceptUpdate(x => x.EndDateTime);
            }

            if (userContext.IsEqual(task.ToEmployee) && task.Status.Code == StatusCode.InProcess)
            {
                taskResult.AcceptUpdate(x => x.Result);
            }

            return repositoryContext.UpdateTask(taskResult).ToDelta();
          }

        public IDelta<ITask> GetTask(Guid id)
        {


            if (id == Guid.Empty)
            {
                return null;
            }


            IDelta<ITask> taskResult = repositoryContext.GetTask(id).ToDelta().AsReadOnly();


            if (userContext.IsEqualOr(taskResult.Instance.Author, taskResult.Instance.Manager))
            {
                taskResult.AsReadWrite(x => x.Priority)
                          .AsReadWrite(x => x.Description);
            }



            switch (taskResult.Instance.Status.Code)
            {
                case StatusCode.Pending : if(userContext.IsEqual(taskResult.Instance.Author))
                {
                    taskResult
                     .AsReadWrite(x => x.Name)
                     .AsReadWrite(x => x.Objective)
                     .AsReadWrite(x => x.StartDateTime)
                     .AsReadWrite(x => x.EndDateTime);
                }
                break;

                case StatusCode.InProcess: if (userContext.IsEqual(taskResult.Instance.ToEmployee))
                {
                        taskResult.AsReadWrite(x => x.Result);
                }
                break;
            }


            return taskResult;
        }

        public IEnumerable<ITask> GetMyTasks()
        {
            return repositoryContext.GetTasksByEmployee(10, userContext.Sid, StatusCode.Pending,StatusCode.InProcess, StatusCode.Revision);
        }

        private Guid? CreateSid()
        {
            if (userContext.Logged.IsAdmin)
            {
                return null;
            }

            return userContext.Sid;
        }

        public IPage<ITask> GetTasksByFilter(ITaskFilter filter)
        {
            return repositoryContext.ApplyTaskFilterOnly(CreateSid(), filter);
        }

        public ICompositeTaskSet GetCompositeTasks(ITaskFilter filter)
        {
            ICompositeTaskSet taskSet = repositoryContext.ApplyTaskFilterWithCompute(CreateSid(), filter);

            return new CompositeTaskSet
            {
                LoggedUser = userContext.Logged,
                Page = taskSet.Page,
                Priorities = clsService.GetPriorities()
                                       .Merge(taskSet.Priorities, x => x.Id, x => x.Id, (x, y) => new AggregateClassifier { Id = x.Id, Name = x.Name, Total = y.Total }, x => new AggregateClassifier { Id = x.Id, Name = x.Name, Total = 0 })
                                       .ToArray(),

                Statuses = clsService.GetStatuses()
                                       .Merge(taskSet.Statuses, x => x.Id, x => x.Id, (x, y) => new AggregateClassifier { Id = x.Id, Name = x.Name, Total = y.Total }, x => new AggregateClassifier { Id = x.Id, Name = x.Name, Total = 0 })
                                       .ToArray(),

                Users = clsService.GetSystemUsers().Merge(taskSet.Users, x => x.Id, x => x.Id, (x, y) => new UserObject { Id = x.Id, Name = x.Name, TotalTasks = y.TotalTasks }, x => new UserObject(x)).ToArray()

            };

        }
    }
}
