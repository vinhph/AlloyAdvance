using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace AlloyAdvanced.Business.Validation
{
    // Example regular expression for strong password:
    // ^                                Start anchor
    // (?=.*[A-Z].*[A-Z])               Ensure string has two uppercase letters.
    // (?=.*[!@#$&*])                   Ensure string has one special case letter.
    // (?=.*[0-9].*[0-9])           Ensure string has two digits.
    // (?=.*[a-z].*[a-z].*[a-z])  Ensure string has three lowercase letters.
    // .{8,}                            Ensure string is of length 8 minimum.
    // $                                End anchor.

    public class StrongPassword : ValidationAttribute
    {
        public string SpecialCharacters { get; set; } = "!@#$&*";

        public int MinimumTotalCharacters { get; set; } = 8;

        public int MinimumUppercaseLetters { get; set; } = 1;
        public int MinimumLowercaseLetters { get; set; } = 1;
        public int MinimumDigitCharacters { get; set; } = 1;
        public int MinimumSpecialCharacters { get; set; } = 1;

        public override bool IsValid(object value)
        {
            ErrorMessage = $"Password must have: {MinimumDigitCharacters} digits, {MinimumSpecialCharacters} special characters, {MinimumUppercaseLetters} upper case, {MinimumLowercaseLetters} lower case, and {MinimumTotalCharacters} total length.";

            string input = value as string;

            var builder = new StringBuilder();

            builder.Append("^");

            if (MinimumUppercaseLetters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumUppercaseLetters; i++)
                {
                    builder.Append(".*[A-Z]");
                }
                builder.Append(")");
            }

            if (MinimumLowercaseLetters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumLowercaseLetters; i++)
                {
                    builder.Append(".*[a-z]");
                }
                builder.Append(")");
            }

            if (MinimumSpecialCharacters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumSpecialCharacters; i++)
                {
                    builder.Append(".*[");
                    builder.Append(SpecialCharacters);
                    builder.Append("]");
                }
                builder.Append(")");
            }

            if (MinimumDigitCharacters > 0)
            {
                builder.Append("(?=");
                for (int i = 0; i < MinimumDigitCharacters; i++)
                {
                    builder.Append(".*[0-9]");
                }
                builder.Append(")");
            }

            builder.Append(".{");
            builder.Append(MinimumTotalCharacters);
            builder.Append(",}$");

            return Regex.IsMatch(input, builder.ToString());
        }
    }
}