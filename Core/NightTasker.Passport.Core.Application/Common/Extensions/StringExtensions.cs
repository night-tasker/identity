using System.Text;

namespace NightTasker.Passport.Application.Common.Extensions;

public static class StringExtensions
{
    public static string ConvertCamelToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var result = new StringBuilder();
        bool lastWasUpper = false;
        bool lastWasNumeric = false;

        for (int i = 0; i < input.Length; i++)
        {
            char currentChar = input[i];

            if (char.IsUpper(currentChar))
            {
                if (!lastWasUpper && i > 0)
                {
                    result.Append('_');
                }
                lastWasUpper = true;
            }
            else if (char.IsDigit(currentChar))
            {
                if (!lastWasNumeric && i > 0)
                {
                    result.Append('_');
                }
                lastWasNumeric = true;
                lastWasUpper = false;
            }
            else
            {
                lastWasUpper = false;
                lastWasNumeric = false;
            }

            result.Append(currentChar);
        }

        return result.ToString().ToLower();
    }
}