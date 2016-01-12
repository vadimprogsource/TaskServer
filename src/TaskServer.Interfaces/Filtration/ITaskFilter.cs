using System;

namespace TaskServer.Interfaces.Filtration
{
    public interface ITaskFilter
    {
        DateTime? StartCreatedDate { get;  }
        DateTime? EndCreatedDate   { get;  }
        int? PriorityCode { get;  }
        string SearchText { get;  }   

        int PageIndex { get; }
        int PageSize { get; }
    }
}
