using System.Data.Entity.ModelConfiguration;
using Teachersteams.Domain.Entities;

namespace Teachersteams.DataAccess.Mappings
{
    public class GroupMap: EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            HasKey(t => t.Id);

            Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            ToTable("Group");
            Property(t => t.Title).HasColumnName("Title").IsRequired();
            Property(t => t.Description).HasColumnName("Description").IsOptional();
            Property(t => t.CreateDate).HasColumnName("CreateDate").IsRequired();
            Property(t => t.OwnerId).HasColumnName("OwnerId").IsRequired();
            Property(t => t.PictureLink).HasColumnName("PictureLink").IsOptional();
        }
    }
}
