using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FluentAssertions;
using SqlServer.Management.IntegrationServices.Testing;
using Xbehave;
using Xunit;

namespace SqlServer.Management.IntegrationServices
{
    public class SsisCatalogTests
    {
        public class UsingContainer:TestFixtureBase
        {
            [Fact]
            public void CanCreateForConnectionString()
            {
                Action invoker = () => {
                    var catalog = GetInstance<ISsisCatalogFactory>().Create(SsisdbConnectionString);
                };

                invoker.ShouldNotThrow();
            }
        }
    }
}
