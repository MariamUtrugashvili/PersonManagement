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
            entity.HasQueryFilter(p => !p.IsDeleted);

            entity.HasOne(rp => rp.Person)
              .WithMany(p => p.RelatedPersons)
              .HasForeignKey(rp => rp.PersonId)
              .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(rp => rp.RelatedToPerson)
                .WithMany()
                .HasForeignKey(rp => rp.RelatedToPersonId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
