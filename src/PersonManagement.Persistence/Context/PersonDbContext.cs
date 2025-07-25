﻿using Microsoft.EntityFrameworkCore;
using PersonManagement.Domain.Entities;

namespace PersonManagement.Persistence.Context
{
    public class PersonDbContext(DbContextOptions<PersonDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<PhoneNumber> PhoneNumbers { get; set; } = null!;
        public DbSet<RelatedPerson> RelatedPersons { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersonDbContext).Assembly);
        }
    }
}
