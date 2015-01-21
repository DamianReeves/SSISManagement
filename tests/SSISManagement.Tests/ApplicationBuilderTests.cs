using System;
using System.Collections.Generic;
using FluentAssertions;
using SqlServer.Management.IntegrationServices.Configuration;
using Xunit;

namespace SqlServer.Management.IntegrationServices
{
    public class ApplicationBuilderTests
    {
        public ApplicationBuilderTests()
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

        [Fact]
        public void ConfigureRequiresANonNullAction()
        {
            Action<IConfigurationSyntax> configAction = null;
            Builder.Invoking(b => b.Configure(configAction))
                .ShouldThrow<ArgumentNullException>()
                .Where(ex => ex.ParamName == "configurationAction");
        }

        [Fact]
        public void ConfigureShouldAddActionToEndOfConfigurationActionsCollection()
        {
            // Arrange
            Action<IConfigurationSyntax> configAction1 = cfg => { };
            Action<IConfigurationSyntax> configAction2 = cfg => { };

            // Act
            Builder.Configure(configAction1);
            Builder.Configure(configAction2);

            // Assert
            Builder.ConfigurationActions.Should()
                .ContainInOrder(configAction1, configAction2);

        }

        [Fact]
        public void CallingCreateExecutesConfigurationActionsInOrder()
        {
            // Arrange
            var callTracker = new List<int> {};

            Action<IConfigurationSyntax> configAction1 = cfg => { callTracker.Add(1);};
            Action<IConfigurationSyntax> configAction2 = cfg => { callTracker.Add(2); };
            Builder.Configure(configAction1);
            Builder.Configure(configAction2);

            // Act            
            var application = Builder.Create();

            // Assert
            callTracker.Should().ContainInOrder(1, 2);
        }

        [Fact]
        public void CallingCreateWithCustomConfigShouldCreateANonNullApplicationInstance()
        {
            // Arrange
            Action<IConfigurationSyntax> configAction1 = cfg => { };
            Action<IConfigurationSyntax> configAction2 = cfg => { };
            Builder.Configure(configAction1);
            Builder.Configure(configAction2);

            // Act            
            var application = Builder.Create();

            // Assert
            application.Should().NotBeNull();
        }
    }
}