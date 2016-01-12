using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;

namespace TaskServer.Objects
{
    public class AggregateClassifier : Classifier ,IAggregateClassifier
    {
        public int Total { get; set; }
    }
}
