using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SqlServer.Management.IntegrationServices.Core.Services
{
    public class ServiceCollectionTests
    {
        public class WhenConstructedUsingEmptyConstructor
        {
            internal ServiceCollection Sut { get; set; }
            public WhenConstructedUsingEmptyConstructor()
            {
                Sut = new ServiceCollection();
            } 
        }
    }
}
