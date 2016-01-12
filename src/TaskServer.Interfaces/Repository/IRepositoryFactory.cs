using System;

namespace TaskServer.Interfaces.Repository
{
    public interface IRepositoryFactory
    {
        object CreateInstance(Type type);

        T CreateRepository<T>();
    }
}
