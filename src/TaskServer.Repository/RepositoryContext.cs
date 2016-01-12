using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace TaskServer.Repository
{
    public class RepositoryContext : DbContext 
    {
        private RepositoryMapper mapper;

        public RepositoryContext(string nameOrConnectioString,RepositoryMapper mapper ) : base(nameOrConnectioString)
        {
            this.mapper = mapper;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            mapper.MapTo(modelBuilder);
        }
    }
}
