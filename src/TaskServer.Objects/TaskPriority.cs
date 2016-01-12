using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;

namespace TaskServer.Objects
{
    public class TaskPriority : Classifier, IPriority
    {
        public PriorityCode Code
        {
            get
            {
                return (PriorityCode)Id;
            }

            set
            {
                Id = (int)value;
            }
        }

        public TaskPriority()
        {
            Code = PriorityCode.Low;
        }

        public TaskPriority(IPriority outer) : base(outer) { }
    }
}
