using System.Data.Entity.ModelConfiguration;
using Teachersteams.Domain.Entities;

namespace Teachersteams.DataAccess.Mappings
{
    public class StudentMap: EntityTypeConfiguration<Student>
    {
        public StudentMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            ToTable("Student");
            Property(t => t.Uid).HasColumnName("Uid").IsRequired();
            Property(t => t.GroupId).HasColumnName("GroupId").IsRequired();
            Property(t => t.Status).HasColumnName("Status").IsRequired();
        }
    }
}
