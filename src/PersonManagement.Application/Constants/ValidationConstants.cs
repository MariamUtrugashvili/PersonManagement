namespace PersonManagement.Application.Constants
{
    public static class ValidationConstants
    {
        public const string FirstNameRequired = "First name is required.";
        public const string FirstNameLength = "First name must be between 2 and 50 characters.";
        public const string FirstNameLatinOrGeorgian = "First name must contain only Latin or Georgian letters.";
        public const string LastNameRequired = "Last name is required.";
        public const string LastNameLength = "Last name must be between 2 and 50 characters.";
        public const string LastNameLatinOrGeorgian = "Last name must contain only Latin or Georgian letters.";
        public const string GenderInvalid = "Gender is invalid.";
        public const string PersonalNumberRequired = "Personal number is required.";
        public const string PersonalNumberFormat = "Personal number must be exactly 11 digits.";
        public const string DateOfBirth18 = "Person must be at least 18 years old.";
        public const string PhoneNumberTypeInvalid = "Phone number type is invalid.";
        public const string PhoneNumberLength = "Phone number must be between 4 and 50 characters.";
        public const string InvalidPersonId = "Invalid person ID.";
    }
}
