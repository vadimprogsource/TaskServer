using System;
using System.Linq;
using TaskServer.Interfaces;

namespace TaskServer.Objects
{
    public class TaskObject : BaseObject, ITask
    {

        public UserObject Author { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string Name { get; set; }
        public string Objective { get; set; }
        public UserObject Manager { get; set; }
        public TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }
        public UserObject ToEmployee { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        public string Result { get; set; }
        public TaskObject RootTask { get; set; }
        public TaskObject[] SubTasks { get; set; }

        IUser ITask.Author
        {
            get
            {
                return Author;
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
                return RootTask;
            }
        }

        ITask[] ITask.SubTasks
        {
            get
            {
                return SubTasks;
            }
        }

        public TaskObject()
        {
            StartDateTime = (CreatedDateTime = DateTime.Now).AddDays(1);
            EndDateTime   = StartDateTime.AddDays(7);

            Status   = new TaskStatus();
            Priority = new TaskPriority();
        }


        public TaskObject(ITask outer)
        {
            CreatedDateTime = outer.CreatedDateTime;
            Name = outer.Name; 
            Objective = outer.Objective;


            if (outer.Author != null)
            {
                Author = new UserObject(outer.Author);
            }

            if (outer.Manager != null)
            {
                Manager = new UserObject(outer.Manager);
            }


            if (outer.Status != null)
            {
                Status = new TaskStatus(outer.Status);
            }

            if (outer.Priority != null)
            {
                Priority = new TaskPriority(outer.Priority);
            }


            if (outer.ToEmployee != null)
            {
                ToEmployee = new UserObject(outer.ToEmployee);
            }


            StartDateTime = outer.StartDateTime;
            EndDateTime = outer.EndDateTime;

            Description = outer.Description;
            Result = outer.Result;

            if (outer.RootTask != null)
            {
                RootTask = new TaskObject(outer.RootTask);
            }

            if (outer.SubTasks != null)
            {
                SubTasks = outer.SubTasks.Select(x => new TaskObject(x)).ToArray();
            }
        }



    }
}
