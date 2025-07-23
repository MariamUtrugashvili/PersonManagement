using Swashbuckle.AspNetCore.Filters;
using PersonManagement.Application.Persons.Commands.UpdatePerson;
using PersonManagement.Domain.Enums;
using System.Collections.Generic;
using PersonManagement.Application.Persons.Common;

namespace PersonManagement.Api.Examples
{
    public class UpdatePersonCommandExample : IExamplesProvider<UpdatePersonCommand>
    {
        public UpdatePersonCommand GetExamples()
        {
            return new UpdatePersonCommand
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(2003, 1, 1),
                PersonalNumber = "12345678910",
                Gender = Gender.Male,
                PhoneNumbers =
                [
                    new PhoneNumberRequest { PhoneNumberType = PhoneNumberType.Mobile, Number = "555123123" }
                ]
            };
        }
    }
}
