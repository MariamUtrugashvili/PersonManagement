using PersonManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Domain.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public string Number { get; private set; } = null!;
        public PhoneNumberType PhoneNumberType { get; private set; }

        protected PhoneNumber() { } // EF

        public PhoneNumber(PhoneNumberType phoneNumberType, string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Phone number cannot be empty", nameof(number));

            PhoneNumberType = phoneNumberType;
            Number = number;
        }

        public void UpdateNumber(string newNumber)
        {
            if (string.IsNullOrWhiteSpace(newNumber))
                throw new ArgumentException("Phone number cannot be empty", nameof(newNumber));

            Number = newNumber;
            SetUpdated();
        }
    }
}
