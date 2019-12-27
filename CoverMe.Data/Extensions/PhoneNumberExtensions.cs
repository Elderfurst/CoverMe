using PhoneNumbers;

namespace CoverMe.Data.Extensions
{
    public static class PhoneNumberExtensions
    {
        public static PhoneNumber ParsePhoneNumber(this string phoneNumber, string countryCode)
        {
            // Get the standard E.164 phone number parser
            var phoneNumberParser = PhoneNumberUtil.GetInstance();

            return phoneNumberParser.Parse(phoneNumber, countryCode.ToUpper());
        }

        public static PhoneNumber ParsePhoneNumber(this ulong? phoneNumber, string countryCode)
        {
            if (phoneNumber == null)
            {
                return null;
            }

            return phoneNumber.ToString().ParsePhoneNumber(countryCode.ToUpper());
        }
    }
}
