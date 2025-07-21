using System;
using System.Globalization;
using System.Resources;

namespace PersonManagement.Domain.Exceptions
{
    public class PersonNotFoundException : Exception
    {
        public PersonNotFoundException(int id)
            : base(GetMessage(id))
        {
        }

        private static string GetMessage(int id)
        {
            var resourceManager = PersonManagement.Domain.Resources.ExceptionMessages.ResourceManager;
            var message = resourceManager.GetString("PersonNotFound", CultureInfo.CurrentCulture);
            return string.Format(message ?? "Person with id {0} not found.", id);
        }
    }
}
