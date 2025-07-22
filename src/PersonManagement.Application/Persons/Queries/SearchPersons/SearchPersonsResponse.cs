using PersonManagement.Domain.Enums;

namespace PersonManagement.Application.Persons.Queries.SearchPersons
{
    public class SearchPersonsResponse
    {
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public List<PersonResponseModel> Items { get; set; } = [];
    }

    public class PersonResponseModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; } = null!;
    }
}