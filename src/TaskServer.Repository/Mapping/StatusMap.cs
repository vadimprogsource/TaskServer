using System.Data.Entity.ModelConfiguration;
using TaskServer.Entities;

namespace TaskServer.Repository.Mapping
{
    public class StatusMap : EntityTypeConfiguration<StatusEntity>
    {
        public StatusMap()
        {
            ToTable("TaskStatuses");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("Id");
            Property(x => x.Name).HasMaxLength(50).HasColumnName("Name");

            Ignore(x => x.Code);

        }
    }
}
