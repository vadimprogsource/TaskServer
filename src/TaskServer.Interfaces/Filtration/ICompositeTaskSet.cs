using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Filtration
{
    public interface ICompositeTaskSet: ICompositeResultSet<ITask>
    {

        IUser LoggedUser { get; }

        IEnumerable<IAggregateClassifier> Statuses   { get; }
        IEnumerable<IAggregateClassifier> Priorities { get; }

        IEnumerable<IUser> Users { get; }

    }
}
