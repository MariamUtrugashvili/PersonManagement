using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Configurations
{
    public class RelatedPersonConfiguration : IEntityTypeConfiguration<RelatedPerson>
    {
        public void Configure(EntityTypeBuilder<RelatedPerson> entity)
        {
            entity.ToTable("RelatedPersons");
            entity.HasKey(rp => rp.Id);
            entity.Property(rp => rp.RelationType).IsRequired();
        }
    }
}
