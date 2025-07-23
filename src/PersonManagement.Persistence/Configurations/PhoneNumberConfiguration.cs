using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Configurations
{
    public class PhoneNumberConfiguration : IEntityTypeConfiguration<PhoneNumber>
    {
        public void Configure(EntityTypeBuilder<PhoneNumber> entity)
        {
            entity.ToTable("PhoneNumbers");
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Number).IsRequired().HasMaxLength(50);
            entity.Property(p => p.PhoneNumberType).IsRequired();
            entity.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
