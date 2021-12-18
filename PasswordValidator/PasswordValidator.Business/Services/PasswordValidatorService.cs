using PasswordValidator.Business.DTOs;
using PasswordValidator.Business.Enums;
using PasswordValidator.Business.Extensions;
using PasswordValidator.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Business.Services
{
    public class PasswordValidatorService : IPasswordValidatorService
    {
        public PasswordValidationResult Validate(string password)
        {
            return new PasswordValidationResult()
            {
                Success = false,
                Errors = new List<PasswordValidationErrorEnum>() { PasswordValidationErrorEnum.RepeatedChars, PasswordValidationErrorEnum.NoDigits }
            };
        }
    }
}
