using System.Data.Entity.ModelConfiguration;
using Teachersteams.Domain.Entities;

namespace Teachersteams.DataAccess.Mappings
{
    public class AssignmentMap: EntityTypeConfiguration<Assignment>
    {
        public AssignmentMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            ToTable("Assignment");
            Property(t => t.Title).HasColumnName("Title").IsRequired();
            Property(t => t.Description).HasColumnName("Description").IsOptional();
            Property(t => t.Status).HasColumnName("Status").IsRequired();
            Property(t => t.ExpirationDate).HasColumnName("ExpirationDate").IsRequired();
            Property(t => t.File).HasColumnName("File").IsRequired();
            Property(t => t.GroupId).HasColumnName("GroupId").IsRequired();
            Property(t => t.Creator).HasColumnName("Creator").IsOptional();

            HasRequired(t => t.Group).WithMany().HasForeignKey(t => t.GroupId);
            HasMany(t => t.Results).WithRequired().HasForeignKey(t => t.AssignmentId);
        }
    }
}
