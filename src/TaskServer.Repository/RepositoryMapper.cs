using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace TaskServer.Repository
{
    public class RepositoryMapper
    {
        private readonly List<Type> mapping = new List<Type>();


        public RepositoryMapper Add(IEnumerable<Type> mapType)
        {
            mapping.AddRange(mapType);
            return this;
        }

        public RepositoryMapper AddWhere(Assembly asm, Func<Type, bool> predicate)
        {
            return Add(asm.GetTypes().Where(predicate).Where(x => x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>)));
        }


        public RepositoryMapper UseDefault()
        {
            return AddWhere(GetType().Assembly, x => true);
        }


        
        
        public RepositoryMapper MapTo(DbModelBuilder modelBuilder)
        {

            foreach (Type t in mapping)
            {
                dynamic instance = Activator.CreateInstance(t);
                modelBuilder.Configurations.Add(instance);
            }

            return this;

        }


        internal DbModelBuilder GetModelBuilder()
        {
            DbModelBuilder modelBuilder = new DbModelBuilder(DbModelBuilderVersion.Latest);
            MapTo(modelBuilder);

            return modelBuilder;
        }

    }
}
