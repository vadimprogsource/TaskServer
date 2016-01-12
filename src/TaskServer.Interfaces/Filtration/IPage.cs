using System.Collections.Generic;

namespace TaskServer.Interfaces.Filtration
{
    public interface IPage<T> : IEnumerable<T>
    {
        int PageIndex { get;  }
        int PageSize { get;  }
        int Pages { get;  }
    }
}
