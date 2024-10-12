using Autofac;

namespace BtgPactual.Back.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
