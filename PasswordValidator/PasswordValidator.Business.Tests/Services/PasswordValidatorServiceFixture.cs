using Moq.AutoMock;
using PasswordValidator.Business.Interfaces;
using PasswordValidator.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PasswordValidator.Business.Tests.Services
{
    [CollectionDefinition(nameof(PasswordValidatorServiceCollection))]
    public class PasswordValidatorServiceCollection : ICollectionFixture<PasswordValidatorServiceFixture>
    {
    }

    public class PasswordValidatorServiceFixture : IDisposable
    {
        public IPasswordValidatorService PasswordValidatorService;
        public AutoMocker Mocker;

        public IPasswordValidatorService GetPasswordValidatorService()
        {
            Mocker = new AutoMocker();
            PasswordValidatorService = Mocker.CreateInstance<PasswordValidatorService>();

            return PasswordValidatorService;
        }

        public void Dispose()
        {
        }
    }
}
