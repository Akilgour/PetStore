using Autofac;
using PetStore.Data.Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore.StockDelivery.Manager.Autofac
{
    public class StockDeliveryManagerModule : Module
    {
        public StockDeliveryManagerModule()
        {

        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new PetStoreDataModule());

            builder.RegisterAssemblyTypes(ThisAssembly)
                .Where(t => t.Name.EndsWith("Manager"))
                .AsImplementedInterfaces();
        }

    }
}


