using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TaskServer.Interfaces.Specialized;

namespace TaskServer.Core.Specialized
{
    public class Delta<TObject> : IDelta<TObject>
    {

        private Dictionary<MemberInfo, Property> propertyCache = new Dictionary<MemberInfo, Property>();



        private Property GetPropertyByLambda(LambdaExpression lambda)
        {
            Property x;

            if (propertyCache.TryGetValue((lambda.Body as MemberExpression).Member, out x))
            {
                return x;
            }

            throw new NotSupportedException();

        }

   


        public Delta(TObject instance)
        {
            propertyCache = typeof(TObject).GetProperties()
                                           .Union(typeof(TObject).GetInterfaces().SelectMany(x => x.GetProperties()))
                                           .ToDictionary(x => x as MemberInfo,x=>new Property(this,x));
            Instance      = instance;
        }


        public object GetInstance() { return Instance; }

        public TObject Instance
        {
            get;
        }


        public bool HasErrors
        {
            get;
            private set;
        }


        public IDelta<TObject> AcceptUpdate<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            GetPropertyByLambda(propertyOrField).CanUpdate = true;
            return this;
        }

        public bool CanUpdate<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            return GetPropertyByLambda(propertyOrField).CanUpdate;
        }



        public IDelta<TObject> ApplyThrowError<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            HasErrors = true;
            GetPropertyByLambda(propertyOrField).IsError = true;
            return this;
        }

        public IDelta<TObject> AsReadOnly()
        {
            foreach (Property x in propertyCache.Values)
            {
                x.IsReadOnly = true;
            }

            return this;
        }

        public IDelta<TObject> AsReadOnly<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            GetPropertyByLambda(propertyOrField).IsReadOnly = true;
            return this;

        }

        public bool IsReadOnly<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            return GetPropertyByLambda(propertyOrField).IsReadOnly;
        }


        public bool CanReadWrite<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            return !GetPropertyByLambda(propertyOrField).IsReadOnly;
        }


        public bool IsError<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            return GetPropertyByLambda(propertyOrField).IsError;
        }



        public IDelta<TObject> AsReadWrite()
        {
            foreach (Property x in propertyCache.Values)
            {
                x.IsReadOnly = false;
            }

            return this;
        }

        public IDelta<TObject> AsReadWrite<TValue>(Expression<Func<TObject, TValue>> propertyOrField)
        {
            GetPropertyByLambda(propertyOrField).IsReadOnly = false;
            return this;
        }

        public IEnumerable<IProperty> GetChangedProperties()
        {
            return propertyCache.Values.Where(x => x.CanUpdate);
        }

        public IEnumerator<IProperty> GetEnumerator()
        {
            return propertyCache.Values.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
