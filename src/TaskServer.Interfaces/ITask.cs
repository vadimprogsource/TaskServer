using System;

namespace TaskServer.Interfaces
{
    public interface ITask : INamedEntity
    {
        IUser Author { get; }
        DateTime CreatedDateTime { get; }
        string Objective            { get; }
        IUser Manager            { get; }
        IStatus Status           { get; }
        IPriority Priority       { get; }
        IUser ToEmployee { get; }
        DateTime StartDateTime { get; }
        DateTime EndDateTime { get; }
        string Description { get;  }
        string Result { get; }
        ITask     RootTask { get; }
        ITask[]   SubTasks { get; }
    }
}
