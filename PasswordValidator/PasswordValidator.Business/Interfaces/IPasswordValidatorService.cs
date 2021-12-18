using PasswordValidator.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Business.Interfaces
{
    public interface IPasswordValidatorService
    {
        public PasswordValidationResult Validate(string password);
    }
}
