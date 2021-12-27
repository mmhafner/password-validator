using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PasswordValidator.Api.Tests.Configuration
{
    [CollectionDefinition(nameof(IntegrationTestsFixtureCollection))]
    public class IntegrationTestsFixtureCollection : ICollectionFixture<IntegrationTestsFixture<StartupTests>> { }

    public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
    {
        public readonly PasswordValidatorApiFactory<TStartup> Factory;
        public HttpClient Client { get; private set; }

        public IntegrationTestsFixture()
        {
            Factory = new PasswordValidatorApiFactory<TStartup>();
            Client = Factory.CreateClient();
            Client.SetJsonMediaType();
        }

        public void Dispose()
        {
            Client?.Dispose();
            Factory?.Dispose();
        }
    }
}
