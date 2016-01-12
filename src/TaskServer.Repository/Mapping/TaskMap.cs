using System.Data.Entity.ModelConfiguration;
using TaskServer.Entities;

namespace TaskServer.Repository.Mapping
{
    public class TaskMap : EntityTypeConfiguration<TaskEntity>
    {
        public TaskMap()
        {
            ToTable("Tasks");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("TaskId");
            Property(x => x.ParentTaskId).HasColumnName("ParentTaskId");
            Property(x => x.Name).HasMaxLength(100).HasColumnName("TaskName");
            Property(x => x.Objective).HasMaxLength(100).HasColumnName("TaskTarget");
            Property(x => x.Result).HasMaxLength(100).HasColumnName("TaskResult");

            Property(x => x.CreatedDateTime).HasColumnName("TaskCreatedDateTime");
            Property(x => x.StartDateTime  ).HasColumnName("TaskStartDateTime");
            Property(x => x.EndDateTime    ).HasColumnName("TaskEndDateTime");

            Property(x => x.AuthorId  ).HasColumnName("AuthorId");
            Property(x => x.EmployeeId).HasColumnName("EmployeeId");
            Property(x => x.ManagerId ).HasColumnName("ManagerId");

            Property(x => x.PriorityId).HasColumnName("TaskPriorityId");
            Property(x => x.StatusId  ).HasColumnName("TaskStatusId"  );

            Property(x => x.Description).HasMaxLength(100).HasColumnName("TaskDescription");

         //   HasOptional(x => x.ParentTask).WithMany().HasForeignKey(x => x.ParentTaskId);
            HasRequired(x => x.Priority  ).WithMany().HasForeignKey(x => x.PriorityId);
            HasRequired(x => x.Status).WithMany().HasForeignKey(x => x.StatusId);
            HasRequired(x => x.Author    ).WithMany().HasForeignKey(x => x.AuthorId);

            HasRequired(x => x.ToEmployee).WithMany().HasForeignKey(x => x.EmployeeId);

            Ignore(x => x.ChildTasks)
           .Ignore(x=>x.ParentTask);



        }
    }
}
