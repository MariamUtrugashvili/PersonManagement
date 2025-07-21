using Microsoft.EntityFrameworkCore;
using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using PersonManagement.Domain.Repositories;
using PersonManagement.Domain.Exceptions;
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
            IQueryable<Person> query = _context.Persons.AsNoTracking();

            if (includePhoneNumbers)
                query = query.Include(p => p.PhoneNumbers);
            if (includeRelatedPersons)
                query = query.Include(p => p.RelatedPersons);

            return await query.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);
        }
        public async Task<IReadOnlyList<Person>> SearchAsync(string? firstName, string? lastName, string? personalNumber, int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<Person> query = _context.Persons.AsNoTracking().Where(p => !p.IsDeleted);

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

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken);
            if (person == null)
                throw new PersonNotFoundException(id);
            return true;
        }
        #endregion Reads

        #region Writes
        public async Task AddAsync(Person person, CancellationToken cancellationToken)
        {
            await _context.Persons.AddAsync(person, cancellationToken);
        }

        public async Task UpdateAsync(int id, string firstName, string lastName, Gender gender, string personalNumber, DateTime dateOfBirth, IEnumerable<(PhoneNumberType type, string number)> phoneNumbers, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .Include(p => p.PhoneNumbers)
                .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken) 
                ?? throw new PersonNotFoundException(id);

            person.UpdatePersonalInfo(firstName, lastName, dateOfBirth, gender);

            // Remove all old phone numbers
            var existingNumbers = person.PhoneNumbers.Select(pn => pn.Number).ToList();
            foreach (var number in existingNumbers)
                person.RemovePhoneNumber(number);
            
            // Add new phone numbers
            foreach (var (type, number) in phoneNumbers)
                person.AddPhoneNumber(type, number);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new PersonNotFoundException(id);

            // Soft delete
            person.MarkAsDeleted();
        }
        #endregion Writes

        #endregion Person

        #region Related Person
        public async Task AddRelatedPersonAsync(int personId, int relatedToPersonId, RelationType relationType, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .Include(p => p.RelatedPersons)
                .FirstOrDefaultAsync(p => p.Id == personId && !p.IsDeleted, cancellationToken) 
                ?? throw new PersonNotFoundException(personId);

            person.AddRelatedPerson(relatedToPersonId, relationType);
        }

        public async Task RemoveRelatedPersonAsync(int personId, int relatedToPersonId, CancellationToken cancellationToken)
        {
            var person = await _context.Persons
                .Include(p => p.RelatedPersons)
                .FirstOrDefaultAsync(p => p.Id == personId && !p.IsDeleted, cancellationToken) 
                ?? throw new PersonNotFoundException(personId);
            
            person.RemoveRelatedPerson(relatedToPersonId);
        }
        #endregion Related Person

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
