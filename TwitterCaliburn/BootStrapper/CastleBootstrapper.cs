using Castle.Facilities.TypedFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using TwitterCaliburn.ViewModels;

namespace TwitterCaliburn
{
    public class CastleBootstrapper : Bootstrapper<MainViewModel>
    {
        private ApplicationContainer _container;

        protected override void Configure()
        {
            _container = new ApplicationContainer();
            _container.AddFacility<TypedFactoryFacility>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return string.IsNullOrWhiteSpace(key)
            ? _container.Resolve(service)
            : _container.Resolve(key, service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return (IEnumerable<object>)_container.ResolveAll(service);
        }

        protected override void BuildUp(object instance)
        {
            instance.GetType().GetProperties()
            .Where(property => property.CanWrite && property.PropertyType.IsPublic)
            .Where(property => _container.Kernel.HasComponent(property.PropertyType))
            .ForEach(property => property.SetValue(instance, _container.Resolve(property.PropertyType), null));
        }
    }
}
