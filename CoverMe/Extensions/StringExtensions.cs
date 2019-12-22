namespace CoverMe.Extensions
{
    public static class StringExtensions
    {
        public static ulong ParsePhoneNumber(this string phoneNumber, string countryCode)
        {
            // Get the standard E.164 phone number parser
            var phoneNumberParser = PhoneNumbers.PhoneNumberUtil.GetInstance();

            var parsedPhoneNumber = phoneNumberParser.Parse(phoneNumber, countryCode);

            return parsedPhoneNumber.NationalNumber;
        }
    }
}
