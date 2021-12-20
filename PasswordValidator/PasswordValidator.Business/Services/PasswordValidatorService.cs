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
            var result = new PasswordValidationResult() { Success = true };
            string pattern = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[!|@|#|$|%|^|&|*|(|)|\-|+])(?=\S+$).{9,}$";
            if(!Regex.Matches(password, pattern, RegexOptions.Singleline).Any())
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

            if (password.Length < 9)
            {
                errors.Add(PasswordValidationErrorEnum.LessThanNineChars);
            }
            if (!password.Any(char.IsDigit))
            {
                errors.Add(PasswordValidationErrorEnum.NoDigits);
            }
            if (!password.Any(char.IsLower))
            {
                errors.Add(PasswordValidationErrorEnum.NoLowercaseLetters);
            }
            if (!password.Any(char.IsUpper))
            {
                errors.Add(PasswordValidationErrorEnum.NoUppercaseLetters);
            }
            if (!password.Any(letter => specialCaracters.Any(specialChar => specialChar == letter)))
            {
                errors.Add(PasswordValidationErrorEnum.NoSpecialChars);
            }
            if (password.Length != password.Distinct().Count())
            {
                errors.Add(PasswordValidationErrorEnum.RepeatedChars);
            }
            return errors;
        }
    }
}
