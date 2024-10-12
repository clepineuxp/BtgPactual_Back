using Autofac;

namespace BtgPactual.Back.Core
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CoreModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
