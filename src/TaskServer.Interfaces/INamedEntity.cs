using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{
   public interface INamedEntity : IEntity
    {
         string Name { get;  }
    }
}
