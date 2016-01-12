using System.Data.Entity.ModelConfiguration;
using TaskServer.Entities;

namespace TaskServer.Repository.Mapping
{
    public class UserMap : EntityTypeConfiguration<UserEntity>
    {
        public UserMap()
        {
            ToTable("UserAccounts");
            HasKey(x => x.Id);
            Property(x => x.Id).HasColumnName("UserId");
            Property(x => x.Role).HasColumnName("UserRole");
            Property(x => x.Name     ).HasMaxLength(50).HasColumnName("UserName");
            Property(x => x.LoginName).HasMaxLength(50).HasColumnName("UserLoginName");
            Property(x => x.Password).HasColumnName("UserPasswordHash");

            Ignore(x => x.IsUser).Ignore(x => x.IsPowerUser).Ignore(x => x.IsAdmin).Ignore(x=>x.TotalTasks);
        }
    }
}
