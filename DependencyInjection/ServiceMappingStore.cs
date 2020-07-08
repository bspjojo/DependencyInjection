using System;
using System.Collections.Generic;

namespace BSP.Dependency.Injection.DependencyInjection
{
    public class ServiceMappingStore
    {
        private Dictionary<Type, InjectionBindingType> _bindingTypeForTInterface;
        private Dictionary<Type, Type> _bindingsSingleton;
        private Dictionary<Type, Type> _bindingsScoped;
        private Dictionary<Type, Type> _bindingsTransient;

        public ServiceMappingStore()
        {
            _bindingTypeForTInterface = new Dictionary<Type, InjectionBindingType>();
            _bindingsSingleton = new Dictionary<Type, Type>();
            _bindingsScoped = new Dictionary<Type, Type>();
            _bindingsTransient = new Dictionary<Type, Type>();
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

        public Type GetImplementationTypeForInterface(Type iType)
        {
            var ibt = _bindingTypeForTInterface[iType];

            switch (ibt)
            {
                case InjectionBindingType.Singleton:
                    return _bindingsSingleton[iType];

                case InjectionBindingType.Scoped:
                    return _bindingsScoped[iType];

                case InjectionBindingType.Transient:
                    return _bindingsTransient[iType];

                default:
                    throw new Exception();
            }
        }

        private void AddBinding<TInterface, TImplementation>(InjectionBindingType ibt) where TImplementation : TInterface
        {
            _bindingTypeForTInterface.Add(typeof(TInterface), ibt);

            switch (ibt)
            {
                case InjectionBindingType.Singleton:
                    _bindingsSingleton.Add(typeof(TInterface), typeof(TImplementation));
                    break;

                case InjectionBindingType.Scoped:
                    _bindingsScoped.Add(typeof(TInterface), typeof(TImplementation));
                    break;

                case InjectionBindingType.Transient:
                    _bindingsTransient.Add(typeof(TInterface), typeof(TImplementation));
                    break;
            }
        }
    }

    public enum InjectionBindingType
    {
        Singleton,
        Scoped,
        Transient
    }
}