using System;
using System.Data.Entity;
using TaskServer.Interfaces.Repository;

namespace TaskServer.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private string           nameOrConnectionString;
        private RepositoryMapper mapper;

        static RepositoryFactory()
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
            Database.SetInitializer<DbContext>(null);
            Database.SetInitializer<RepositoryContext>(null);

        }


        public RepositoryFactory(string nameOrConnectionString)
        {
            this.nameOrConnectionString = nameOrConnectionString;
            mapper = new RepositoryMapper().UseDefault();
        }

        internal DbContext CreateContext()
        {
            return new RepositoryContext(nameOrConnectionString, mapper);
        }


        public T CreateRepository<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        public object CreateInstance(Type type)
        {

            if (type == typeof(IUserRepository))
            {
                return new Repositories.UserRepository(this);
            }


            if (type == typeof(IClassifierRepository))
            {
                return new Repositories.ClassifierRepository(this);
            }

            if (type == typeof(ITaskRepository))
            {
                return new Repositories.Task.TaskRepository(this);
            }

            throw new NotImplementedException();
        }
    }
}
