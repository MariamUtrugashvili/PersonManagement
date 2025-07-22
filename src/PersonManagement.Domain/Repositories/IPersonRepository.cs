using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;

namespace PersonManagement.Domain.Repositories
{
    public interface IPersonRepository
    {
        #region Person
        Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken, bool includePhoneNumbers = true, bool includeRelatedPersons = true);
        Task<Person?> GetByIdNoTrackingAsync(int id, CancellationToken cancellationToken, bool includePhoneNumbers = true, bool includeRelatedPersons = true);
        Task<IReadOnlyList<Person>> SearchAsync(string? firstName, string? lastName, string? personalNumber, int pageNumber, int pageSize, CancellationToken cancellationToken);
        Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> ExistsByPersonalNumberAsync(string personalNumber, CancellationToken cancellationToken);

        Task AddAsync(Person person, CancellationToken cancellationToken);
        #endregion

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}