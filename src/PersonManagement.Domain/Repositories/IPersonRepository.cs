using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Domain.Repositories
{
    public interface IPersonRepository
    {
        #region Person
        Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken, bool includePhoneNumbers = true, bool includeRelatedPersons = true);
        Task<IReadOnlyList<Person>> SearchAsync(string? firstName, string? lastName, string? personalNumber, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
        Task AddAsync(Person person, CancellationToken cancellationToken);
        Task UpdateAsync(int id, string firstName, string lastName, Gender gender, string personalNumber, DateTime dateOfBirth, IEnumerable<(PhoneNumberType type, string number)> phoneNumbers, CancellationToken cancellationToken);
        Task DeleteAsync(int id, CancellationToken cancellationToken);
        #endregion

        #region Related Person
        Task AddRelatedPersonAsync(int personId, int relatedToPersonId, RelationType relationType, CancellationToken cancellationToken);
        Task RemoveRelatedPersonAsync(int personId, int relatedToPersonId, CancellationToken cancellationToken);
        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}