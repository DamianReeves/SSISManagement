using SqlServer.Management.IntegrationServices.LightInject;
namespace SqlServer.Management.IntegrationServices.Testing
{
    public abstract class TestRequiringContainer
    {
        internal IServiceContainer Container { get; private set; }

        protected TestRequiringContainer()
        {
            var builder = new ApplicationBuilder();
            Container = builder.CreateContainer();
        }

        public T GetInstance<T>()
        {
            return Container.GetInstance<T>();
        }

        public T Create<T>() where T:class
        {
            return Container.Create<T>();
        }
    }
}