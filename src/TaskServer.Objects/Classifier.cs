using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskServer.Interfaces;

namespace TaskServer.Objects
{
    public class Classifier : IClassifier
    {
         public int Id      { get; set; }
         public string Name { get; set; }

        public Classifier() { }

        public Classifier(IClassifier outer)
        {
            Id   = outer.Id;
            Name = outer.Name; 
        }
    }
}
