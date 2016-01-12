using System;
using TaskServer.Interfaces;

namespace TaskServer.Dto
{
    public class EntityDto : IEntity
    {
        public Guid Id { get; set; }

        public EntityDto() { }

        public EntityDto(IEntity other)
        {
            Id = other.Id;
        }


    }
}
