using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using TaskServer.Interfaces.Specialized;

namespace TaskServer.Dto.Specialized
{
    public class DtoDelta<T> : DynamicObject
    {
        private Dictionary<string, DtoProperty> propertyCache;


        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return propertyCache.Keys.OrderBy(x => x).Union(new[] {"HasError"});
        }


        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            DtoProperty x;

            if (propertyCache.TryGetValue(binder.Name, out x))
            {
                result = x;
                return true;
            }


            if (binder.Name == "HasError")
            {
                result = hasErrors;
                return true;
            }


            result = null;
            return false;

        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {

            DtoProperty x;

            if (propertyCache.TryGetValue(binder.Name, out x))
            {
                x.Value = value;
                return true;

            }


                return false;
        }

        private readonly bool hasErrors;


        public DtoDelta(IDelta<T> obj)
        {
            hasErrors     = obj.HasErrors;
            propertyCache = obj.ToDictionary(x=>x.Name,x => new DtoProperty(x));
        }

        public DtoDelta()
        {
            propertyCache = typeof(T).GetProperties().Where(x => x.CanWrite).ToDictionary(x => x.Name, x => new DtoProperty());
        }


        private DtoProperty GetPropertyByLambda(LambdaExpression lambda)
        {
            DtoProperty x;

            if (propertyCache.TryGetValue((lambda.Body as MemberExpression).Member.Name, out x))
            {
                return x;
            }

            return null;

        }

        private void ReplaceByLambda(LambdaExpression lambda, Func<DtoProperty,DtoProperty> createProperty)
        {
            string key = (lambda.Body as MemberExpression).Member.Name;

            DtoProperty x;

            if (propertyCache.TryGetValue(key, out x))
            {
                propertyCache[key] = createProperty(x);
            }

        }


        public DtoDelta<T> AddPropertyValues<V>(Expression<Func<T, V>> propertyOrField, IEnumerable<V> values)
        {
            ReplaceByLambda(propertyOrField, x => new DtoProperty<V>(x, values.ToArray()));
            return this;
        }


    }
}
