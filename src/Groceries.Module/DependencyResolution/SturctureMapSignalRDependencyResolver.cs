namespace Groceries.Module.DependencyResolution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.AspNet.SignalR;

    using StructureMap;

    using Web.Contracts.Extensions;

    public class SturctureMapSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IContainer _container;

        public SturctureMapSignalRDependencyResolver(IContainer container)
        {
            container.Guard();

            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType) ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            var objs = _container.GetAllInstances(serviceType).Cast<object>();
            return objs.Concat(base.GetServices(serviceType));
        }
    }
}