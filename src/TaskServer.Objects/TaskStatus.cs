using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;

namespace TaskServer.Objects
{
    public class TaskStatus : Classifier, IStatus
    {
        public StatusCode Code
        {
            get
            {
                return (StatusCode)Id;
            }

            set
            {
                Id = (int)value;
            }
        }

        public TaskStatus(IStatus outer) : base(outer) { }

        public TaskStatus()
        {
            Code = StatusCode.Pending;
        }
    }
}
