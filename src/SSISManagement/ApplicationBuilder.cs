

using System;
using System.CodeDom;
using System.Collections.Generic;
using SqlServer.Management.IntegrationServices.Configuration;
using SqlServer.Management.IntegrationServices.LightInject;

namespace SqlServer.Management.IntegrationServices
{
    public class ApplicationBuilder
    {
        private readonly IList<Action<IConfigurationSyntax>> _configurationActions;

        public ApplicationBuilder()
        {
            _configurationActions = new List<Action<IConfigurationSyntax>>();
        }

        internal IList<Action<IConfigurationSyntax>> ConfigurationActions
        {
            get { return _configurationActions; }
        }

        public ApplicationBuilder Configure(Action<IConfigurationSyntax> configurationAction)
        {
            if (configurationAction == null) throw new ArgumentNullException("configurationAction");
            _configurationActions.Add(configurationAction);
            return this;
        }

        public ISsisApplication Create()
        {            
            return CreateContainer().GetInstance<ISsisApplication>();
        }

        internal IServiceContainer CreateContainer()
        {
            var container = new ServiceContainer();
            var configurator = new ConfigurationSyntax(new RegistrationSyntax(container));
            ApplyDefaultConfiguration(container);
            ApplyCustomConfiguration(configurator);
            return container;
        }

        private void ApplyDefaultConfiguration(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterFrom<SsisCompositionRoot>();
        }

        private void ApplyCustomConfiguration(IConfigurationSyntax configurator)
        {            
            foreach (var configurationAction in ConfigurationActions)
            {
                configurationAction(configurator);

            }
        }
    }
}