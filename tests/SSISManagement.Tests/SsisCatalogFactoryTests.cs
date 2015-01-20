using FluentAssertions;
using SqlServer.Management.IntegrationServices.LightInject;
using SqlServer.Management.IntegrationServices.Testing;
using Xunit;

namespace SqlServer.Management.IntegrationServices
{
    public class SsisCatalogFactoryTests 
    {
        public class UsingContainerConfiguration : TestRequiringConnectionStrings
        {
            internal IServiceContainer Container { get; private set; }
            public UsingContainerConfiguration()
            {
                var builder = new ApplicationBuilder();
                Container = builder.CreateContainer();
            }
            [Fact]
            public void CanCreateCatalog()
            {
                var sut = Container.Create<SsisCatalogFactory>();
                var catalog = sut.Create(SsisdbConnectionString);
                catalog.Should().NotBeNull();
            }
        }        
    }
}