using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;

namespace TaskServer.Entities
{
    public class AggregateClassifierEntity : ClassifierEntity, IAggregateClassifier
    {
        public int Total { get; set; }
    }
}
