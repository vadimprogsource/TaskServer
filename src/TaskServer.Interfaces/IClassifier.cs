using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskServer.Interfaces
{
    public  interface IClassifier
    {
        int Id { get; }
        string Name { get; }
    }
}
