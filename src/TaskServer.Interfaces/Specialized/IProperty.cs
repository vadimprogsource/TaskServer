using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Specialized
{
    public interface IProperty
    {
        bool IsReadOnly { get; }
        bool IsError    { get; }
        bool CanUpdate { get; }

        string Name { get; }
        object Value { get; }


    }
}
