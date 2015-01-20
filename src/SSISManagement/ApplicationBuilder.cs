

using System;
using System.Collections.Generic;
using SqlServer.Management.IntegrationServices.LightInject;

namespace SqlServer.Management.IntegrationServices
{
    public class ApplicationBuilder
    {
        private readonly IList<Action<IServiceRegistry>> _registrationActions;

        public ApplicationBuilder()
        {
            _registrationActions = new List<Action<IServiceRegistry>>
            {
                ApplyDefaultConfiguration
            };
        }

        internal IList<Action<IServiceRegistry>> RegistrationActions 
        {
            get { return _registrationActions; }
        }

        public ISsisApplication Create()
        {            
            return CreateContainer().GetInstance<ISsisApplication>();
        }

        internal IServiceContainer CreateContainer()
        {
            var container = new ServiceContainer();
            foreach (var action in RegistrationActions)
            {
                action(container);
            }
            return container;
        }

        private void ApplyDefaultConfiguration(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ISsisApplication, SsisApplication>();
            serviceRegistry.Register<ISsisCatalogFactory, SsisCatalogFactory>(new PerContainerLifetime());
        }
    }
}