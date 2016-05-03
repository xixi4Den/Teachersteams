using System.Data.Entity.ModelConfiguration;
using Teachersteams.Domain.Entities;

namespace Teachersteams.DataAccess.Mappings
{
    public class AssignmentResultMap: EntityTypeConfiguration<AssignmentResult>
    {
        public AssignmentResultMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            ToTable("AssignmentResult");
            Property(t => t.AssignmentId).HasColumnName("AssignmentId").IsRequired();
            Property(t => t.StudentId).HasColumnName("StudentId").IsRequired();
            Property(t => t.CompletionDate).HasColumnName("CompletionDate").IsRequired();
            Property(t => t.File).HasColumnName("File").IsRequired();
            Property(t => t.AssigneeTeacherId).HasColumnName("AssigneeTeacherId").IsOptional();
            Property(t => t.Grade).HasColumnName("Grade").IsOptional();
            Property(t => t.CheckDate).HasColumnName("CheckDate").IsOptional();

            HasRequired(t => t.Assignment).WithMany().HasForeignKey(t => t.AssignmentId);
            HasRequired(t => t.Student).WithMany().HasForeignKey(t => t.StudentId);
            HasOptional(t => t.AssigneeTeacher).WithMany().HasForeignKey(t => t.AssigneeTeacherId);
        }
    }
}
