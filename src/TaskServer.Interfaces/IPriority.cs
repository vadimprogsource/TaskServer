using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{

    public enum PriorityCode : byte  {Low=1,Medium=2,High=3};

    public interface IPriority  : IClassifier
    {
        PriorityCode Code { get; }
    }
}
