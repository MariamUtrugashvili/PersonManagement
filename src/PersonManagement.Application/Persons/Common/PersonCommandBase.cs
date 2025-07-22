using PersonManagement.Domain.Entities;
using PersonManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Common
{
    public abstract class PersonCommandBase
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string PersonalNumber { get; set; } = null!;
        public Gender Gender { get; set; }
        public List<PhoneNumberRequest> PhoneNumbers { get; set; } = [];
    }

    public class PhoneNumberRequest
    {
        public PhoneNumberType PhoneNumberType { get; set; }
        public string Number { get; set; } = null!;
    }
}
