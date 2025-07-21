using PersonManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string FirstName { get; private set; } = null!;
        public string LastName { get; private set; } = null!;
        public DateTime DateOfBirth { get; private set; } 
        public Gender Gender { get; private set; }
        public string PersonalNumber { get; private set; } = null!;

        private readonly List<RelatedPerson> _relatedPersons = [];
        public IReadOnlyCollection<RelatedPerson> RelatedPersons => _relatedPersons.AsReadOnly();

        private readonly List<PhoneNumber> _phoneNumbers = [];
        public IReadOnlyCollection<PhoneNumber> PhoneNumbers => _phoneNumbers.AsReadOnly();

        protected Person() { } //For EF

        public Person(string firstName, string lastName, DateTime dateOfBirth, string personalNumber, string city, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PersonalNumber = personalNumber;
        }

    }
}
