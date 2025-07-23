using Microsoft.EntityFrameworkCore;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;
using PersonManagement.Persistence.Context;

namespace PersonManagement.Persistence.Repositories
{
    public class PersonRepository(PersonDbContext context) : IPersonRepository
    {
        private readonly PersonDbContext _context = context;

        #region Person

        #region Reads
        public async Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken, bool includePhoneNumbers = true, bool includeRelatedPersons = true)
        {
            IQueryable<Person> query = _context.Persons;

            if (includePhoneNumbers)
                query = query.Include(p => p.PhoneNumbers);
            if (includeRelatedPersons)
                query = query.Include(p => p.RelatedPersons);

            return await query.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Person?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken, bool includePhoneNumbers = true, bool includeRelatedPersons = true)
        {
            IQueryable<Person> query = _context.Persons.AsNoTracking();

            if (includePhoneNumbers)
                query = query.Include(p => p.PhoneNumbers);
            if (includeRelatedPersons)
                query = query.Include(p => p.RelatedPersons)
                         .ThenInclude(rp => rp.RelatedToPerson)
                         .ThenInclude(rtp => rtp.PhoneNumbers);


            return await query.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<Person>> SearchAsync(string? firstName, string? lastName, string? personalNumber, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Person> query = _context.Persons.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(firstName))
            {
                query = query.Where(p => p.FirstName.Contains(firstName));
            }

            if (!string.IsNullOrWhiteSpace(lastName))
            {
                query = query.Where(p => p.LastName.Contains(lastName));
            }

            if (!string.IsNullOrWhiteSpace(personalNumber))
            {
                query = query.Where(p => p.PersonalNumber.Contains(personalNumber));
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
        {
            var exists = await _context.Persons.AnyAsync(p => p.Id == id, cancellationToken);
            return exists;
        }

        public async Task<bool> ExistsByPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken)
        {
            var exists = await _context.Persons.AnyAsync(p => p.PersonalNumber == personalNumber, cancellationToken);
            return exists;
        }
        #endregion Reads

        #region Writes
        public async Task AddAsync(Person person, CancellationToken cancellationToken)
        {
            await _context.Persons.AddAsync(person, cancellationToken);
        }
        #endregion Writes

        #endregion Person

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
