using System;
using TaskServer.Interfaces;

namespace TaskServer.Entities
{
    public class Entity : IEntity
    {
        public Guid Id { get; set; }
    }
}
