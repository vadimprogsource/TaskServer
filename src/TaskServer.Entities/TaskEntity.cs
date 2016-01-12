using System;
using TaskServer.Interfaces;

namespace TaskServer.Entities
{
    public class TaskEntity : NamedEntity, ITask
    {
        public DateTime CreatedDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public Guid? ParentTaskId { get; set; }
        public Guid     AuthorId        { get; set; }
        public Guid ManagerId { get; set; }
    
        public Guid     EmployeeId      { get; set; } 
        public int PriorityId { get; set; }
        public int StatusId { get; set; }
        public string Objective { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        public UserEntity Author { get; set; }
        public UserEntity Manager { get; set; }
        public UserEntity ToEmployee { get; set; }
        public StatusEntity Status { get; set; }
        public PriorityEntity Priority { get; set; }
        public TaskEntity   ParentTask { get; set; }
        public TaskEntity[] ChildTasks { get; set; } 


        IUser ITask.Author
        {
            get
            {
                return Author;
            }
        }

        IPriority ITask.Priority
        {
            get
            {
                return Priority;
            }
        }

        IUser ITask.ToEmployee
        {
            get
            {
                return ToEmployee;
            }
        }

        ITask ITask.RootTask
        {
            get
            {
                return ParentTask;
            }
        }

        ITask[] ITask.SubTasks
        {
            get
            {
                return ChildTasks;
            }
        }

        IUser ITask.Manager
        {
            get
            {
                return Manager;
            }
        }

        IStatus ITask.Status
        {
            get
            {
                return Status;
            }
        }
    }
}
