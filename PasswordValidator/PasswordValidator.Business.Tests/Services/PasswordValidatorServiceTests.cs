using FluentAssertions;
using Moq.AutoMock;
using PasswordValidator.Business.Interfaces;
using PasswordValidator.Business.Tests.Services.Fixtures;
using Xunit;

namespace PasswordValidator.Business.Tests.Services
{
    [Collection(nameof(PasswordValidatorServiceCollection))]
    public class PasswordValidatorServiceTests
    {
        readonly PasswordValidatorServiceFixture _passwordValidatorServiceFixture;
        private readonly IPasswordValidatorService _passwordValidatorService;
        public PasswordValidatorServiceTests(PasswordValidatorServiceFixture passwordValidatorServiceFixture)
        {
            _passwordValidatorServiceFixture = passwordValidatorServiceFixture;
            _passwordValidatorService = _passwordValidatorServiceFixture.GetPasswordValidatorService();
        }

        [Theory(DisplayName = "Validate - Valid Passwords")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("123qweEWQ#@!")]
        [InlineData("qwe123EWQ#@!")]
        [InlineData("#@!qweEWQ123")]
        [InlineData("aBc123#@9")]
        [InlineData("123456aB!")]
        public void PasswordValidatorService_Validate_MustBeValid(string password)
        {
            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeTrue();
            result.Errors.Should().BeNullOrEmpty();
        }

        [Theory(DisplayName = "Validate - Invalid Passwords")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("12qwEW!@")]
        [InlineData("qweEWQ#@!")]
        [InlineData("123EWQ#@!")]
        [InlineData("123eqw#@!")]
        [InlineData("123qweEWQ")]
        [InlineData("#@!qqweEWQ123")]
        [InlineData("aaBc123#@1")]
        public void PasswordValidatorService_Validate_MustFailBecausePasswordIsInvalid(string password)
        {
            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
        }

        [Theory(DisplayName = "Validate - Passwords Less Than Nine Characters")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("12qwWQ@!")]
        [InlineData("qwe")]
        [InlineData("#@!qweE")]
        [InlineData("aB")]
        [InlineData("125#6aB!")]
        public void PasswordValidatorService_Validate_MustFailBecausePasswordIsLessThanNineChars(string password)
        {

            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Errors.Should().Contain(Enums.PasswordValidationErrorEnum.LessThanNineChars);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Digits")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("qweEWQ#@!")]
        [InlineData("qwWQ#@!")]
        [InlineData("#@!qweEWQ")]
        [InlineData("aBc#@DEfgh")]
        [InlineData("abcdEFG#@!B")]
        public void PasswordValidatorService_Validate_MustFailBecausePasswordHasNoDigits(string password)
        {
            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Errors.Should().Contain(Enums.PasswordValidationErrorEnum.NoDigits);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Lowercase Letters")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("123EWQ#@!")]
        [InlineData("12WQ#@!")]
        [InlineData("#@!123EWQ")]
        [InlineData("1B3#@DE456")]
        [InlineData("12345EFG#@!B")]
        public void PasswordValidatorService_Validate_MustFailBecausePasswordHasNoLowerCaseLetters(string password)
        {
            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Errors.Should().Contain(Enums.PasswordValidationErrorEnum.NoLowercaseLetters);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Uppercase Letters")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("qwe123#@!")]
        [InlineData("qw12#@!")]
        [InlineData("#@!qwe123")]
        [InlineData("a3c#@12fgh")]
        [InlineData("abcd456#@!2")]
        public void PasswordValidatorService_Validate_MustFailBecausePasswordHasNoUpperCaseLetters(string password)
        {
            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Errors.Should().Contain(Enums.PasswordValidationErrorEnum.NoUppercaseLetters);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Special Characters")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("qweEWQ123")]
        [InlineData("qwWQ123")]
        [InlineData("456qweEWQ")]
        [InlineData("aBc12DEfgh")]
        [InlineData("abcdEFG345B")]
        public void PasswordValidatorService_Validate_MustFailBecausePasswordHasNoSpecialChars(string password)
        {
            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Errors.Should().Contain(Enums.PasswordValidationErrorEnum.NoSpecialChars);
        }

        [Theory(DisplayName = "Validate - Passwords Has Repeated Chars")]
        [Trait("Unit", "Business - PasswordValidatorService")]
        [InlineData("qweEWQqwe#@!")]
        [InlineData("q@wWQ#@!")]
        [InlineData("#@!qEweEWQ")]
        [InlineData("aBc#@DEf$#gh")]
        [InlineData("abc!dEFBG#@!B")]
        public void PasswordValidatorService_Validate_MustFailBecausePasswordHasRepeatedChars(string password)
        {
            //Arrange && Act
            var result = _passwordValidatorService.Validate(password);
            //Assert
            result.Success.Should().BeFalse();
            result.Errors.Should().HaveCountGreaterThan(0);
            result.Errors.Should().Contain(Enums.PasswordValidationErrorEnum.RepeatedChars);
        }
    }
}
