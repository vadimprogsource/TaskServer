using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskServer.Interfaces.Specialized
{


    public interface IDelta : IEnumerable<IProperty>
    {
        object GetInstance();

        bool HasErrors { get; }
        IEnumerable<IProperty> GetChangedProperties();
    }

    public interface IDelta<TObject> : IDelta
    {
        TObject Instance { get; }
        IDelta<TObject> AsReadWrite();
        IDelta<TObject> AsReadWrite<TValue>(Expression<Func<TObject, TValue>> propertyOrField);
        IDelta<TObject> AsReadOnly();
        IDelta<TObject> AsReadOnly<TValue>(Expression<Func<TObject, TValue>> propertyOrField);
        bool IsReadOnly<TValue>(Expression<Func<TObject, TValue>> propertyOrField);
        bool CanReadWrite<TValue>(Expression<Func<TObject, TValue>> propertyOrField);
        IDelta<TObject> ApplyThrowError<TValue>(Expression<Func<TObject, TValue>> propertyOrField);
        bool IsError<TValue>(Expression<Func<TObject, TValue>> propertyOrField);
        IDelta<TObject> AcceptUpdate<TValue>(Expression<Func<TObject, TValue>> propertyOrField);
        bool CanUpdate<TValue>(Expression<Func<TObject, TValue>> propertyOrField);

    }
}
