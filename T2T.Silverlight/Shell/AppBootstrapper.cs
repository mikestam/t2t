using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using Caliburn.Micro;
using Caliburn.Micro.Navigation;

namespace T2T
{
    public class AppBootstrapper: Bootstrapper<IShell>
    {
        CompositionContainer container;

        protected override void Configure()
        {
            var catalog = new AggregateCatalog(
              AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).OfType<ComposablePartCatalog>()
              );

            container = CompositionHost.Initialize(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue<INavigationService>(new CaliburnNavigationService());
       
            batch.AddExportedValue(container);
            batch.AddExportedValue(catalog);
    
            //        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(x => typeof(Screen).IsAssignableFrom(x)).Named<Screen>(x => x.GetNavigationName());
            //NavigationConvention.AddNavigationConventions();

            container.Compose(batch);

        }

      

        protected override object GetInstance(Type serviceType, string key)
        {
           
                string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
                var exports = container.GetExportedValues<object>(contract);

                if (exports.Count() > 0)
                    return exports.First();

                throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
          
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override void BuildUp(object instance)
        {
            container.SatisfyImportsOnce(instance);
        }

     
    }

}
