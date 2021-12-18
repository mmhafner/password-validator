using PasswordValidator.Business.Enums;
using System.Collections.Generic;

namespace PasswordValidator.Business.DTOs
{
    public class PasswordValidationResult
    {
        public bool Success { get; set; }
        public IList<PasswordValidationErrorEnum> Errors { get; set; }
    }
}
