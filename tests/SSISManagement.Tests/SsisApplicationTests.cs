using System;
using System.Collections.Generic;
using FakeItEasy;
using Xbehave;
using Xunit.Extensions;

namespace SqlServer.Management.IntegrationServices
{
    public class SsisApplicationTests
    {

        [Scenario, PropertyData("WhenGettingCatalogByConnectionStringOrNameData")]
        public void WhenGettingCatalogByConnectionStringOrName(string connectionStringOrName, ISsisApplication application, ISsisCatalogFactory catalogFactory, ISsisCatalog expectedCatalog, ISsisCatalog catalog)
        {
            "Given an ISsisApplication instance"
                ._(() =>
                {
                    catalogFactory = A.Fake<ISsisCatalogFactory>();
                    expectedCatalog = A.Fake<ISsisCatalog>();
                    A.CallTo(() => catalogFactory.Create(connectionStringOrName)).Returns(expectedCatalog);
                    application = new SsisApplication(catalogFactory);
                });

            "When getting catalog by connection string or name"
                ._(() => catalog = application.GetCatalog(connectionStringOrName));

            "Then it should have used the catalog factory to create the catalog"
                ._(() => 
                    A.CallTo(()=>catalogFactory.Create(connectionStringOrName))
                        .MustHaveHappened(Repeated.Exactly.Once));

        }

        public static IEnumerable<object[]> WhenGettingCatalogByConnectionStringOrNameData
        {
            get
            {
                yield return new object[]{"name=SSISDB"};
            }
        }
    }
}