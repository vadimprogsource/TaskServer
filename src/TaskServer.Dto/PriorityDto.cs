using System;
using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class PriorityDto : ClassifierDto, IPriority
    {
        public PriorityDto() { }



        public PriorityDto(IPriority other) : base(other)
        {
        }

        public PriorityCode Code
        {
            get
            {
                return (PriorityCode)Id;
            }
        }
    }
}
