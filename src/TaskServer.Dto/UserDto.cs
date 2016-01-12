using System;
using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class UserDto : NamedEntityDto,IUser
    {
        public UserDto() { }

        public UserDto(IUser other) : base(other)
        {
            TotalTasks = other.TotalTasks;
        }

        public int TotalTasks
        {
            get;
        }
    }
}
