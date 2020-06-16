using Autofac;
using PetStore.Data.Context;
using PetStore.Data.Factorys;
using System.Linq;

namespace PetStore.Data.Autofac
{
    public class PetStoreDataModule : Module
    {
        public PetStoreDataModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .WithParameter("context", new PetStoreContext())
                .WithParameter("retryPolicy", PollyFactory.CreateAsyncRetryPolicy())
                .AsImplementedInterfaces();
        }
    }
}