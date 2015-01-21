using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlServer.Management.IntegrationServices.LightInject;

namespace SqlServer.Management.IntegrationServices.Configuration
{
    public interface IRegistrationSyntax : IFluentInterface
    {
        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TImplementation">The implementing type.</typeparam>
        void Register<TService, TImplementation>() where TImplementation : TService;
        /// <summary>
        /// Registers the <typeparamref name="TService"/> with the <typeparamref name="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The service type to register.</typeparam>
        /// <typeparam name="TImplementation">The implementing type.</typeparam>
        /// <param name="serviceName">The name of the service.</param>
        void Register<TService, TImplementation>(string serviceName) where TImplementation : TService;
    }

    internal class RegistrationSyntax : IRegistrationSyntax
    {
        private readonly IServiceRegistry _serviceRegistry;

        public RegistrationSyntax(IServiceRegistry serviceRegistry)
        {
            if (serviceRegistry == null) throw new ArgumentNullException("serviceRegistry");
            _serviceRegistry = serviceRegistry;
        }

        public IServiceRegistry ServiceRegistry
        {
            get { return _serviceRegistry; }
        }

        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            ServiceRegistry.Register<TService,TImplementation>();
        }

        public void Register<TService, TImplementation>(string serviceName) where TImplementation : TService
        {
            ServiceRegistry.Register<TService,TImplementation>(serviceName);
        }
    }
}
