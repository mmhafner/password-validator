using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Business.Enums
{
    enum PasswordValidationRuleEnum
    {
        NineOrMoreChars,
        AtLeastOneDigit,
        AtLeastOneLowercaseLetter,
        AtLeastOneUppercaseLetter,
        //Consider !@#$%^&*()-+ as special characters
        AtLeastOneSpecialChar,
        NoRepeatedChars
    }
}
