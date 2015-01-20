using System;
using System.Collections.Generic;
using FluentAssertions;
using SqlServer.Management.IntegrationServices.Data;
using SqlServer.Management.IntegrationServices.Testing;
using Xunit;
using Xunit.Extensions;

namespace SqlServer.Management.IntegrationServices
{
    public class CatalogRepositoryTests
    {
        public class ByHand
        {
            [Fact]
            public void CreatingRequiresNonNullConnectionStringResolver()
            {
                Action ctor = () =>
                {
                    IConnectionStringResolver connectionStringResolver = null;
                    var sut = new CatalogRepositoryFactory(connectionStringResolver);
                };

                ctor.ShouldThrow<ArgumentNullException>()
                    .Where(x => x.ParamName == "connectionStringResolver");
            }
        }

        public class UsingContainer : TestFixtureBase
        {
            [Fact]
            public void CanConstruct()
            {
                Action invocation = () =>
                {
                    var sut = Create<CatalogRepositoryFactory>();
                };

                invocation.ShouldNotThrow();
            }

            [Theory]
            [PropertyData("CanCallCreateData")]
            public void CanCallCreateUsingConnectionStringOrName(string connectionStringOrName)
            {
                var sut = Create<CatalogRepositoryFactory>();
                var repository = sut.Create(connectionStringOrName);
                repository.Should().NotBeNull();
            }

            [Theory]
            [PropertyData("CanCallCreateData")]
            public void CreateUsingConnectionStringOrNameReturnsAnIFolderRepository(string connectionStringOrName)
            {
                var sut = Create<CatalogRepositoryFactory>();
                var actual = sut.Create(connectionStringOrName);
                actual.Should().BeAssignableTo<ICatalogRepository>();
            }


            public static IEnumerable<object[]> CanCallCreateData
            {
                get
                {
                    yield return new object[] { "name=SSISDB" };
                    yield return new object[] { TestHelper.GetConnectionString() };
                }
            }
        }
    }
}