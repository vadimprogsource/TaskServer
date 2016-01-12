using TaskServer.Core.Specialized;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Specialized;
using TaskServer.Objects;

namespace TaskServer.Core.Helpers
{
    public static class ObjectHelper
    {
        public static IDelta<TObject> ToDelta<TObject>(this TObject @this)
        {
            return new Delta<TObject>(@this);
        }





        public static IDelta<ITask> ChangeStatus(this ITask @this , StatusCode statusCode)
        {
            TaskObject task = new TaskObject(@this);
            task.Status.Code = statusCode;
            return new Delta<ITask>(task).AcceptUpdate(x => x.Status);
       }




        public static IDelta<ITask> AsNewTask(this IDelta<ITask> @this)
        {
            return @this
            .AsReadOnly()
            .AsReadWrite(x => x.Manager)
            .AsReadWrite(x => x.Name)
            .AsReadWrite(x => x.Objective)
            .AsReadWrite(x => x.ToEmployee)
            .AsReadWrite(x => x.Priority)
            .AsReadWrite(x => x.StartDateTime)
            .AsReadWrite(x => x.EndDateTime)
            .AsReadWrite(x => x.Description);
        }

    }
}
