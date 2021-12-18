using System.ComponentModel;

namespace PasswordValidator.Business.Enums
{
    public enum PasswordValidationErrorEnum
    {
        [Description("The password is less than p characters")]
        LessThanNineChars,
        [Description("The password has no digits")]
        NoDigits,
        [Description("The password has no lowercase letter")]
        NoLowercaseLetters,
        [Description("The password has no uppercase letter")]
        NoUppercaseLetters,
        //Consider !@#$%^&*()-+ as special characters
        [Description("The password has no special characters")]
        NoSpecialChars,
        [Description("The password has repeated characters")]
        RepeatedChars
    }
}
