using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{
    public interface IDocument : INamedEntity
    {
        IUser CreatedBy { get; }
        DateTime TimeOnCreated { get; }
        IUser ModifiedBy { get; }
        DateTime TimeOnModified { get; }

        IUser ManagedBy { get; }
        IUser OwnedBy { get; }

    }
}
