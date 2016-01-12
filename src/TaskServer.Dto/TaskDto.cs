
using System;
using System.Linq;
using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class TaskDto : NamedEntityDto, ITask
    {

        public DateTime CreatedDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string Description { get; set; }
        public UserDto Author { get; set; }
        public UserDto Manager { get; set; }
        public UserDto ToEmployee { get; set; }
        public StatusDto Status { get; set; }
        public PriorityDto Priority { get; set; }
        public string Objective { get; set; }
        public string Result { get; set; }
        public TaskDto RootTask { get; set; }
        public TaskDto[] SubTasks { get; set; }


        IUser ITask.Author
        {
            get
            {
                return Author;
            }
        }

        IUser ITask.ToEmployee
        {
            get
            {
                return ToEmployee;
            }
        }

        IPriority ITask.Priority
        {
            get
            {
                return Priority;
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

        public TaskDto() { }

        public TaskDto(ITask other):base(other)
        {
            CreatedDateTime = other.CreatedDateTime;
            StartDateTime = other.StartDateTime;

            EndDateTime = other.EndDateTime;



            Description     = other.Description;
            Author          = new UserDto(other.Author);
            ToEmployee      = new UserDto(other.ToEmployee);
            Manager         = new UserDto(other.Manager);
            Priority        = new PriorityDto(other.Priority);
            Status          = new StatusDto(other.Status);


            Name = other.Name;

            Result = other.Result;
            Objective = other.Objective;



            if (other.Manager != null)
            {
                Manager = new UserDto(other.Manager);
            }

            if(other.RootTask!=null)
            {
                RootTask = new TaskDto(other.RootTask);
            }

            if (other.SubTasks != null)
            {
                SubTasks = other.SubTasks.Select(x => new TaskDto(x)).ToArray();
            }
        }





    }
}
