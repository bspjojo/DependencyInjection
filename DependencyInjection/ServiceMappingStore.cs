using System;
using System.Collections.Generic;

namespace BSP.Dependency.Injection.DependencyInjection
{
    public class ServiceMappingStore
    {
        private Dictionary<Type, InjectionBindingType> _bindingTypeForTInterface;
        private Dictionary<Type, Type> _bindings;

        public ServiceMappingStore()
        {
            _bindingTypeForTInterface = new Dictionary<Type, InjectionBindingType>();
            _bindings = new Dictionary<Type, Type>();
        }

        public void AddSingleton<TInterface, TImplementation>() where TImplementation : TInterface
        {
            AddBinding<TInterface, TImplementation>(InjectionBindingType.Singleton);
        }

        public void AddTransient<TInterface, TImplementation>() where TImplementation : TInterface
        {
            AddBinding<TInterface, TImplementation>(InjectionBindingType.Transient);
        }

        public void AddScoped<TInterface, TImplementation>() where TImplementation : TInterface
        {
            AddBinding<TInterface, TImplementation>(InjectionBindingType.Scoped);
        }

        public Type GetImplementationTypeForBinding(Type iType)
        {
            return _bindings[iType];
        }

        public InjectionBindingType GetScopeForBinding(Type iType)
        {
            return _bindingTypeForTInterface[iType];
        }

        private void AddBinding<TInterface, TImplementation>(InjectionBindingType ibt) where TImplementation : TInterface
        {
            _bindingTypeForTInterface.Add(typeof(TInterface), ibt);
            _bindings.Add(typeof(TInterface), typeof(TImplementation));
        }
    }

    public enum InjectionBindingType
    {
        Singleton, // only ever one
        Scoped, // one instance per resolution
        Transient // new instance each time
    }
}