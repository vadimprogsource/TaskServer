using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using TaskServer.Interfaces.Repository;
using TaskServer.Entities;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Filtration;
using TaskServer.Interfaces.Specialized;

namespace TaskServer.Repository.Repositories.Task
{
    internal class TaskRepository : RepositoryBase, ITaskRepository
    {

        public TaskRepository(RepositoryFactory factory) : base(factory) { }


        public IEnumerable<IPriority> GetTaskPriorities()
        {
            return Context.Set<PriorityEntity>().OrderBy(x => x.Id).ToArray();
        }

        public IEnumerable<IStatus> GetTaskStatuses()
        {
            return Context.Set<StatusEntity>().OrderBy(x => x.Id).ToArray();
        }


        private IQueryable<TaskEntity> SetOfTask()
        {
            return Context.Set<TaskEntity>()
                        .Include("Author")
                        .Include("ToEmployee")
                        .Include("Manager")
                        .Include("Priority")
                        .Include("Status")
                        .AsNoTracking();

        }

        private ITask GetTask(ITask task)
        {
            return SetOfTask().FirstOrDefault(x => x.Id == task.Id) as ITask ?? task;
        }


        public ITask GetTask(Guid taskId)
        {
            return SetOfTask().FirstOrDefault(x => x.Id == taskId);
        }

        private TaskEntity CopyTaskTo(IDelta<ITask> task, TaskEntity taskEntity)
        {

            if(task.CanUpdate(x=>x.Id))
            {
                taskEntity.Id = task.Instance.Id;
            }

            if (task.CanUpdate(x => x.CreatedDateTime))
            {
                taskEntity.CreatedDateTime = task.Instance.CreatedDateTime;
            }

            if (task.CanUpdate(x => x.Author))
            {
                taskEntity.AuthorId = task.Instance.Author.Id;
            }

            if (task.CanUpdate(x => x.Manager))
            {
                taskEntity.ManagerId = task.Instance.Manager.Id;
            }

            if (task.CanUpdate(x => x.ToEmployee))
            {
                taskEntity.EmployeeId = task.Instance.ToEmployee.Id;
            }

            if (task.CanUpdate(x => x.Name))
            {
                taskEntity.Name = task.Instance.Name;
            }

            if (task.CanUpdate(x => x.Objective))
            {
                taskEntity.Objective = task.Instance.Objective;
            }

            if (task.CanUpdate(x => x.Status))
            {
                taskEntity.StatusId = task.Instance.Status.Id;
            }

            if (task.CanUpdate(x => x.Priority))
            {
                taskEntity.PriorityId = task.Instance.Priority.Id;
            }

            if (task.CanUpdate(x => x.StartDateTime))
            {
                taskEntity.StartDateTime = task.Instance.StartDateTime;
            }

            if (task.CanUpdate(x => x.EndDateTime))
            {
                taskEntity.EndDateTime = task.Instance.EndDateTime;
            }

            if (task.CanUpdate(x => x.Result))
            {
                taskEntity.Result = task.Instance.Result;
            }

            if (task.CanUpdate(x => x.Description))
            {
                taskEntity.Description = task.Instance.Description;
            }

            return taskEntity;
        }



        public ITask InsertTask(IDelta<ITask> task)
        {
            CopyTaskTo(task, CreateNew<TaskEntity>());
            Context.SaveChanges();
            return GetTask(task.Instance.Id);
        }


        public ITask UpdateTask(IDelta<ITask> task)
        {
            TaskEntity taskEntity = Context.Set<TaskEntity>().FirstOrDefault(x => x.Id == task.Instance.Id);

            if (taskEntity != null)
            {
                CopyTaskTo(task, taskEntity);
                Context.SaveChanges();
            }

            return GetTask(task.Instance.Id);
        }



        private IQueryable<TaskEntity> ApplyFilter(IQueryable<TaskEntity> query, Guid? sid, ITaskFilter filter)
        {
            if (sid.HasValue)
            {
                query = query.Where(x => x.AuthorId == sid || x.ManagerId == sid || x.EmployeeId == sid);
            }

            if (filter.PriorityCode.HasValue)
            {
                query = query.Where(x => x.PriorityId == filter.PriorityCode);
            }

            if (filter.StartCreatedDate.HasValue)
            {
                DateTime from = filter.StartCreatedDate.Value.Date;
                query = query.Where(x => x.CreatedDateTime >= from);
            }

            if (filter.EndCreatedDate.HasValue)
            {
                DateTime to = filter.EndCreatedDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(x => x.CreatedDateTime <= to);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                query = query.Where(x => x.Name.Contains(filter.SearchText) || x.Description.Contains(filter.SearchText));
            }

            return query.OrderByDescending(x => x.CreatedDateTime);

        }

        public IPage<ITask> ApplyTaskFilterOnly(Guid? sid , ITaskFilter filter)
        {
            return new PageResult<ITask>(filter.PageIndex, filter.PageSize).ExecuteWith(ApplyFilter(SetOfTask(),sid,filter));
        }

        public ICompositeTaskSet ApplyTaskFilterWithCompute(Guid? sid, ITaskFilter filter)
        {
            return new CompositeTaskSet(filter.PageIndex, filter.PageSize).ExecuteWith(ApplyFilter(SetOfTask(), sid, filter));
        }

        public IEnumerable<ITask> GetTasksByEmployee(int maxCount, Guid employeeId, params StatusCode[] statuses)
        {

            int[] statusCodes = statuses.Select(x => (int)x).ToArray();

            return SetOfTask().Where(x => x.EmployeeId == employeeId && statusCodes.Contains(x.StatusId))
                              .OrderByDescending(x => x.PriorityId)
                              .OrderByDescending(x => x.CreatedDateTime)
                              .Take(maxCount)
                              .ToArray();
        }


      
        public ITask GetTaskWithChilds(Guid taskId)
        {
            TaskEntity parentTask = SetOfTask().FirstOrDefault(x => x.Id == taskId);

            if (parentTask != null)
            {
                parentTask.ChildTasks = SetOfTask().Where  (x => x.ParentTaskId == taskId)
                                                   .AsEnumerable()
                                                   .Select(x => 
                                                   {
                                                       x.ParentTask = parentTask;
                                                       return x;
                                                   })   
                                                   .ToArray(); 
            }

            return parentTask;
        }

        public void RemoveTask(Guid taskId)
        {

            var taskSet     = Context.Set<TaskEntity>();

            TaskEntity task = taskSet.FirstOrDefault(x => x.Id == taskId);


            if (task == null ||  taskSet.Any(x => x.ParentTaskId == taskId))
            {
                return;
            }


            taskSet.Remove(task);
            Context.SaveChanges();

        }

    
    }
}
