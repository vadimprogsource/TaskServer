using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Filtration
{
    public interface ICompositeResultSet<T> 
    {
        IPage<T> Page { get; }
    }
}
