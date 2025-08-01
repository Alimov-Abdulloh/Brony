namespace Brony.Extensions;

public static class StringExtensions
{
    public static (string Error, bool IsValid) ValidatePhone(this string phone)
    {
        if (!string.IsNullOrEmpty(phone))
        {
            return ("Phone should not be null or empty", false);
        }

        if (phone.Length != 13)
        {
            return ("Phone number should be 13 characters", false);
        }

        if (!phone.StartsWith("+998"))
        {
            return ("Phone number should start with '+998'", false);
        }
        
        return (string.Empty, true);
    }
}