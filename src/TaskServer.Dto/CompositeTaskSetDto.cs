using System.Linq;
using TaskServer.Dto.Filtration;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Filtration;

namespace TaskServer.Dto
{
    public class CompositeTaskSetDto 
    {
        public UserDto LoggedUser { get; }

        public int PageIndex { get; }
        public int PageSize { get; }

        public TaskDto[] Page { get; }

        public AggregateClassifierDto[] Priorities { get; }
        public AggregateClassifierDto[] Statuses { get; }
        public UserDto[] Users { get; }


        public CompositeTaskSetDto(ICompositeTaskSet other)
        {

            LoggedUser = new UserDto( other.LoggedUser);


            PageIndex = other.Page.PageIndex;
            PageSize  = other.Page.PageSize;
            Page      = other.Page.Select(x => new TaskDto(x)).ToArray();


            Statuses   = other.Statuses.Select  (x => new AggregateClassifierDto(x)).ToArray();
            Priorities = other.Priorities.Select(x => new AggregateClassifierDto(x)).ToArray();
            Users      = other.Users.Select(x => new UserDto(x)).ToArray();
        }


    }
}
