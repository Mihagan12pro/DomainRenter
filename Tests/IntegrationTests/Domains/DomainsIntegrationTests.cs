using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace IntegrationTests.Domains
{
    public class DomainsIntegrationTests : IntegrationTests
    {
        [Fact]
        public async Task Test_Foo()
        {

        }

        public DomainsIntegrationTests(DomainRenterAppFactory<Program> factory) : base(factory)
        {
        }
    }
}
