using System.Text.RegularExpressions;

namespace PersonManagement.Application.Persons.Common
{
    internal static class PersonValidationExtensions
    {
        internal static bool BeOnlyLatinOrGeorgian(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;

            var isLatin = Regex.IsMatch(name, @"^[a-zA-Z]+$");
            var isGeorgian = Regex.IsMatch(name, @"^[ა-ჰ]+$");
            return isLatin ^ isGeorgian;
        }

        internal static bool BeAtLeast18YearsOld(DateTime dob)
        {
            return dob <= DateTime.Today.AddYears(-18);
        }
    }
}
