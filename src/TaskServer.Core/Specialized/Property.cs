using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TaskServer.Interfaces.Specialized;

namespace TaskServer.Core.Specialized
{
    public class Property : IProperty
    {

        private IDelta       ownerPtr ; 
        private PropertyInfo propertyInfo;

        public Property(IDelta owner ,PropertyInfo property)
        {
            ownerPtr     = owner;
            propertyInfo = property;
        }

        public bool CanUpdate { get; internal set; }
        public bool IsError { get; internal set; }
        public bool IsReadOnly { get; internal set; }

        public string Name
        {
            get
            {
                return propertyInfo.Name;
            }
        }

        public object Value
        {
            get
            {
                return propertyInfo.GetValue(ownerPtr.GetInstance());
            }
        }



        public object GetValue(object instancePtr)
        {
            return propertyInfo.GetValue(instancePtr);
        }


        public override int GetHashCode()
        {
            return propertyInfo.GetHashCode();
        }


        public override bool Equals(object obj)
        {
            return obj is Property && ReferenceEquals( (obj as Property).propertyInfo, propertyInfo);
        }
    }
}
