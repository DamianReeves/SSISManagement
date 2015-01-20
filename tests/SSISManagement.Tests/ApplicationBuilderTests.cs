using FluentAssertions;
using Xunit;

namespace SqlServer.Management.IntegrationServices
{
    public class ApplicationBuilderTests
    {
        public class ApllicationBuilderWithDefaultSetupFixture
        {
            public ApllicationBuilderWithDefaultSetupFixture()
            {
                Builder = new ApplicationBuilder();
            }
            public ApplicationBuilder Builder { get; private set; }

            [Fact]
            public void CreateReturnsANonNullInstance()
            {
                var app = Builder.Create();
                app.Should().NotBeNull();
            }

            [Fact]
            public void CreateReturnsAnISsisApplicationInstance()
            {
                var app = Builder.Create();
                app.Should().BeAssignableTo<ISsisApplication>();
            }
        }
    }
}