using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;

namespace TaskServer.Objects
{
    public class UserObject : BaseObject,IUser
    {
        public int TotalTasks { get; set; }

        public string Name { get; set; }

        public UserObject(IUser outer)
        {
            Id   = outer.Id;
            Name = outer.Name;
            TotalTasks = outer.TotalTasks;
        }

        public UserObject() { }
    }
}
