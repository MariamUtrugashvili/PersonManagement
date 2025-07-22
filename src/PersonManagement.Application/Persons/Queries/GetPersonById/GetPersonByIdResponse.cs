using PersonManagement.Domain.Enums;

namespace PersonManagement.Application.Persons.Queries.GetPersonById
{
    public class GetPersonByIdResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; } = null!;
        public List<PhoneNumberResponse> PhoneNumbers { get; set; } = [];

    }
    
    public class PhoneNumberResponse
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public PhoneNumberType PhoneNumberType { get; set; }
    }
}