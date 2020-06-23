using Autofac;
using PetStore.API.Service.Autofac;
using System.Linq;

namespace PetStore.Blazor.WASM.Server.Autofac
{
    public class AutofacConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceModule());

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Manager"))
                .AsImplementedInterfaces();
        }
    }
}