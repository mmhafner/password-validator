using PasswordValidator.Business.DTOs;
using PasswordValidator.Business.Enums;
using PasswordValidator.Business.Extensions;
using PasswordValidator.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PasswordValidator.Business.Services
{
    public class PasswordValidatorService : IPasswordValidatorService
    {
        public PasswordValidationResult Validate(string password)
        {
            password ??= "";
            var result = new PasswordValidationResult() { Success = true };
            string pattern = @"^(?!.*(.).*\1)(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!|@|#|$|%|^|&|*|(|)|\-|+])(?=\S+$).{9,}$";
            if(!Regex.IsMatch(password, pattern, RegexOptions.Singleline))
            {
                result.Success = false;
                result.Errors = GetValidationErrors(password);
            }
            return result;
        }

        private List<PasswordValidationErrorEnum> GetValidationErrors(string password)
        {
            var errors = new List<PasswordValidationErrorEnum>();
            char[] specialCaracters = { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+' };
            password ??= "";
            // Nine or more characters
            if (password.Length < 9)
            {
                errors.Add(PasswordValidationErrorEnum.LessThanNineChars);
            }
            // At least one digit
            if (!password.Any(char.IsDigit))
            {
                errors.Add(PasswordValidationErrorEnum.NoDigits);
            }
            // At least one lowercase letter
            if (!password.Any(char.IsLower))
            {
                errors.Add(PasswordValidationErrorEnum.NoLowercaseLetters);
            }
            // At least one uppercase letter
            if (!password.Any(char.IsUpper))
            {
                errors.Add(PasswordValidationErrorEnum.NoUppercaseLetters);
            }
            // At least one special character (Consider !@#$%^&amp;&#42;()-+ as special characters)
            if (!password.Any(letter => specialCaracters.Any(specialChar => specialChar == letter)))
            {
                errors.Add(PasswordValidationErrorEnum.NoSpecialChars);
            }
            // No repeated characters
            if (password.Length != password.Distinct().Count())
            {
                errors.Add(PasswordValidationErrorEnum.RepeatedChars);
            }
            return errors;
        }
    }
}
