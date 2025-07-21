using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> entity)
        {
            entity.ToTable("Persons");
            entity.HasKey(p => p.Id);
            entity.HasIndex(p => p.FirstName);
            entity.HasIndex(p => p.LastName);
            entity.HasIndex(p => p.PersonalNumber);
            entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
            entity.Property(p => p.PersonalNumber).IsRequired().HasMaxLength(11);
            entity.Property(p => p.Gender).IsRequired();
            entity.Property(p => p.DateOfBirth).IsRequired();
            entity.Property(p => p.IsDeleted).HasDefaultValue(false);
            entity.HasMany(p => p.PhoneNumbers).WithOne().HasForeignKey("PersonId").OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(p => p.RelatedPersons).WithOne().HasForeignKey("PersonId").OnDelete(DeleteBehavior.Cascade);
        }
    }
}
