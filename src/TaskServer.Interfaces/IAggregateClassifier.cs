using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{
    public interface IAggregateClassifier : IClassifier
    {
        int Total { get; }
    }
}
