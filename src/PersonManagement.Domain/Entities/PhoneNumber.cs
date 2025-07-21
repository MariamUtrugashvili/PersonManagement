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

        public PhoneNumber(PhoneNumberType PhoneNumberType, string phoneNumber)
        {
            PhoneNumberType = PhoneNumberType;
            Number = phoneNumber;
        }
    }
}
