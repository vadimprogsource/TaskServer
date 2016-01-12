using System;
using TaskServer.Interfaces.Filtration;

namespace TaskServer.Dto.Filtration
{
    public class TaskFilterDto : ITaskFilter
    {
        public DateTime? StartCreatedDate { get; set; }
        public DateTime? EndCreatedDate { get; set; }
        public int? PriorityCode { get; set; }
        public string SearchText { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }


    }
}
