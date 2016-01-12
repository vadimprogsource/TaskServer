using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaskServer.Interfaces.Specialized;

namespace TaskServer.Dto.Specialized
{
    public class DtoProperty 
    {

        public bool  IsError  { get; set; }
        public bool IsReadOnly{ get; set; }

        public object Value { get; set; }


        public DtoProperty()
        { }

        public DtoProperty(IProperty other)
        {
            IsError    = other.IsError;
            IsReadOnly = other.IsReadOnly;
            Value      = other.Value; 
        }
    }


    public class DtoProperty<T> : DtoProperty
    {
        public T[] ValidValues { get; private set; } 

        public DtoProperty(DtoProperty other , T[] validValues)
        {
            IsError    = other.IsError;
            IsReadOnly = other.IsReadOnly;
            Value      = other.Value;

            ValidValues = validValues;
        }
    }

    
}
