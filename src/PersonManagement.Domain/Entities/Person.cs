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

        protected Person() { } // EF Core

        private Person(string firstName, string lastName, DateTime dateOfBirth, string personalNumber, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PersonalNumber = personalNumber;
            Gender = gender;
        }

        public static Person Create(string firstName, string lastName, DateTime dateOfBirth, string personalNumber, Gender gender)
        {
            return new Person(firstName, lastName, dateOfBirth, personalNumber, gender);
        }

        public void AddPhoneNumber(PhoneNumberType type, string number)
        {
            var phone = PhoneNumber.Create(type, number);
            _phoneNumbers.Add(phone);

            SetUpdated();
        }

        public void RemovePhoneNumber(string number)
        {
            var phone = _phoneNumbers.FirstOrDefault(p => p.Number == number);
            if (phone != null)
            {
                phone.MarkAsDeleted();
                _phoneNumbers.Remove(phone);

                SetUpdated();
            }
        }

        public void AddRelatedPerson(int relatedToPersonId, RelationType relationType)
        {
            var relatedPerson = RelatedPerson.Create(Id, relatedToPersonId, relationType);
            _relatedPersons.Add(relatedPerson);
            SetUpdated();
        }

        public void RemoveRelatedPerson(int relatedToPersonId)
        {
            var relatedPerson = _relatedPersons.FirstOrDefault(rp => rp.RelatedToPersonId == relatedToPersonId);
            if (relatedPerson != null)
            {
                relatedPerson.MarkAsDeleted();
                _relatedPersons.Remove(relatedPerson);

                SetUpdated();
            }
        }

        public void UpdatePersonalInfo(string firstName, string lastName, DateTime dateOfBirth, Gender gender, string personalNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PersonalNumber = personalNumber;
            SetUpdated();
        }
    }

}
