using System.Data.Entity.ModelConfiguration;
using TaskServer.Entities;

namespace TaskServer.Repository.Mapping
{
    public class PriorityMap : EntityTypeConfiguration<PriorityEntity>
    {
        public PriorityMap()
        {
            ToTable("TaskPriorities");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasMaxLength(50).HasColumnName("Name");

            Ignore(x => x.Code);
        }
    }
}
