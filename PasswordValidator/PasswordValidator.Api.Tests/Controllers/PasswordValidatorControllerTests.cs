using PasswordValidator.Api.Tests.Configuration;
using System.Net.Http.Json;
using FluentAssertions;
using Xunit;
using System.Collections.Generic;
using PasswordValidator.Business.Enums;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System;
using PasswordValidator.Api.Model;

namespace PasswordValidator.Api.Tests.Controllers
{
    [Collection(nameof(IntegrationTestsFixtureCollection))]
    public class PasswordValidatorControllerTests
    {
        private readonly IntegrationTestsFixture<StartupTests> _testsFixture;
        private readonly string _requestUrl = "/PasswordValidator";

        public PasswordValidatorControllerTests(IntegrationTestsFixture<StartupTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Theory(DisplayName = "Validate - Valid Passwords")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("123qweEWQ#@!")]
        [InlineData("qwe123EWQ#@!")]
        [InlineData("#@!qweEWQ123")]
        [InlineData("aBc123#@9")]
        [InlineData("123456aB!")]
        public async void PasswordValidatorController_Validate_MustBeValid(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeTrue();
            responseData.Errors.Should().BeNullOrEmpty();
        }

        [Theory(DisplayName = "Validate - Invalid Passwords")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("")]
        [InlineData("12qwEW!@")]
        [InlineData("qweEWQ#@!")]
        [InlineData("123EWQ#@!")]
        [InlineData("123eqw#@!")]
        [InlineData("123qweEWQ")]
        [InlineData("#@!qqweEWQ123")]
        [InlineData("aaBc123#@1")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordIsInvalid(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeFalse();
            responseData.Errors.Should().HaveCountGreaterThan(0);
        }

        [Fact(DisplayName = "Validate - Null Password")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordIsNull()
        {
            //Arrange 
            string password = null;
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);            
        }

        [Theory(DisplayName = "Validate - Passwords Less Than Nine Characters")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("")]
        [InlineData("12qwWQ@!")]
        [InlineData("qwe")]
        [InlineData("#@!qweE")]
        [InlineData("aB")]
        [InlineData("125#6aB!")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordIsLessThanNineChars(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeFalse();
            responseData.Errors.Should().HaveCountGreaterThan(0);
            responseData.Errors.Should().ContainKey((int)PasswordValidationErrorEnum.LessThanNineChars);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Digits")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("qweEWQ#@!")]
        [InlineData("qwWQ#@!")]
        [InlineData("#@!qweEWQ")]
        [InlineData("aBc#@DEfgh")]
        [InlineData("abcdEFG#@!B")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordHasNoDigits(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeFalse();
            responseData.Errors.Should().HaveCountGreaterThan(0);
            responseData.Errors.Should().ContainKey((int)PasswordValidationErrorEnum.NoDigits);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Lowercase Letters")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("123EWQ#@!")]
        [InlineData("12WQ#@!")]
        [InlineData("#@!123EWQ")]
        [InlineData("1B3#@DE456")]
        [InlineData("12345EFG#@!B")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordHasNoLowerCaseLetters(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeFalse();
            responseData.Errors.Should().HaveCountGreaterThan(0);
            responseData.Errors.Should().ContainKey((int)PasswordValidationErrorEnum.NoLowercaseLetters);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Uppercase Letters")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("qwe123#@!")]
        [InlineData("qw12#@!")]
        [InlineData("#@!qwe123")]
        [InlineData("a3c#@12fgh")]
        [InlineData("abcd456#@!2")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordHasNoUpperCaseLetters(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeFalse();
            responseData.Errors.Should().HaveCountGreaterThan(0);
            responseData.Errors.Should().ContainKey((int)PasswordValidationErrorEnum.NoUppercaseLetters);
        }

        [Theory(DisplayName = "Validate - Passwords Has No Special Characters")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("qweEWQ123")]
        [InlineData("qwWQ123")]
        [InlineData("456qweEWQ")]
        [InlineData("aBc12DEfgh")]
        [InlineData("abcdEFG345B")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordHasNoSpecialChars(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeFalse();
            responseData.Errors.Should().HaveCountGreaterThan(0);
            responseData.Errors.Should().ContainKey((int)PasswordValidationErrorEnum.NoSpecialChars);
        }

        [Theory(DisplayName = "Validate - Passwords Has Repeated Characters")]
        [Trait("Integration", "Api - PasswordValidatorController")]
        [InlineData("qweEWQqwe#@!")]
        [InlineData("q@wWQ#@!")]
        [InlineData("#@!qEweEWQ")]
        [InlineData("aBc#@DEf$#gh")]
        [InlineData("abc!dEFBG#@!B")]
        public async void PasswordValidatorController_Validate_MustFailBecausePasswordHasRepeatedChars(string password)
        {
            //Arrange 
            var content = password.ToStringContent();
            //Act
            var response = await _testsFixture.Client.PostAsync(_requestUrl, content);
            //Assert
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<PasswordValidationResultModel>();
            responseData.Success.Should().BeFalse();
            responseData.Errors.Should().HaveCountGreaterThan(0);
            responseData.Errors.Should().ContainKey((int)PasswordValidationErrorEnum.RepeatedChars);
        }
    }
}
