using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;

namespace TaskServer.Objects
{
    public class BaseObject : IEntity
    {
        public Guid Id { get; set; }
    }
}
