using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordValidator.Api.Model
{
    public class PasswordValidationResultModel
    {
        public bool Success { get; set; }
        public Dictionary<int, string> Errors { get; set; }
    }
}
