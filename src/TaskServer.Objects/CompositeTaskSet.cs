using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Filtration;

namespace TaskServer.Objects
{
    public class CompositeTaskSet : ICompositeTaskSet
    {
        public IUser LoggedUser { get; set; }

        public IPage<ITask> Page
        {
            get;
            set;
        }

        public IEnumerable<IAggregateClassifier> Priorities
        {
            get;
            set;
          
        }

        public IEnumerable<IAggregateClassifier> Statuses
        {
            get;
            set;
        }

        public IEnumerable<IUser> Users
        {
            get;
            set;
        }
    }
}
