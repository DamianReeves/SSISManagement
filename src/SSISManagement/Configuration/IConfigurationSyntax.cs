using System;

namespace SqlServer.Management.IntegrationServices.Configuration
{
    public interface IConfigurationSyntax : IFluentInterface
    {
        IRegistrationSyntax ServiceRegistrations { get; }
    }

    internal class ConfigurationSyntax : IConfigurationSyntax
    {
        private readonly IRegistrationSyntax _registrationSyntax;
        
        public ConfigurationSyntax(IRegistrationSyntax registrationSyntax)
        {
            if (registrationSyntax == null) throw new ArgumentNullException("registrationSyntax");
            _registrationSyntax = registrationSyntax;            
        }

        public IRegistrationSyntax ServiceRegistrations
        {
            get { return _registrationSyntax; }
        }
    }
}