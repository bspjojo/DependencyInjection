using System;
using System.Collections.Generic;
using System.Linq;

namespace BSP.Dependency.Injection.DependencyInjection
{
    public class Scope
    {
        private readonly ServiceMappingStore _serviceMappingStore;
        private Dictionary<Type, object> _singletonInstances;

        public Scope(ServiceMappingStore serviceMappingStore)
        {
            _serviceMappingStore = serviceMappingStore;
            _singletonInstances = new Dictionary<Type, object>();
        }

        public T GetInstance<T>()
        {
            return (T)GetObjectInstance(typeof(T), new Dictionary<Type, object>());
        }

        private object GetObjectInstance(Type t, Dictionary<Type, object> scopedInstances)
        {
            var tType = _serviceMappingStore.GetImplementationTypeForBinding(t);
            var injectionScope = _serviceMappingStore.GetScopeForBinding(t);

            var existing = GetExistingInstanceOfTypeForScope(tType, scopedInstances, injectionScope);
            if (existing != null)
            {
                return existing;
            }

            Console.WriteLine($"{t.FullName} to {tType.FullName} in scope {injectionScope}");

            var constructor = tType.GetConstructors().Single();
            var paramInfos = constructor.GetParameters();

            if (paramInfos.Length == 0)
            {
                return CreateParameterlessInstance(tType, scopedInstances, injectionScope);
            }

            var constructorArgs = new List<object>();

            foreach (var p in paramInfos)
            {
                Console.WriteLine(p.ParameterType.FullName);
                constructorArgs.Add(GetObjectInstance(p.ParameterType, scopedInstances));
            }

            return CreateParameterInstance(tType, constructorArgs.ToArray(), scopedInstances, injectionScope);
        }

        private object CreateParameterlessInstance(Type t, Dictionary<Type, object> scopedInstances, InjectionBindingType injectionBindingType)
        {
            Console.WriteLine($"Creating {t.FullName}");
            var instance = Activator.CreateInstance(t);

            StoreInstance(t, instance, scopedInstances, injectionBindingType);

            return instance;
        }

        private object CreateParameterInstance(Type t, object[] paramaters, Dictionary<Type, object> scopedInstances, InjectionBindingType injectionBindingType)
        {
            Console.WriteLine($"Creating {t.FullName} with args");
            var instance = Activator.CreateInstance(t, paramaters);

            StoreInstance(t, instance, scopedInstances, injectionBindingType);

            return instance;
        }

        private void StoreInstance(Type type, object o, Dictionary<Type, object> scopedInstances, InjectionBindingType injectionBindingType)
        {
            switch (injectionBindingType)
            {
                case InjectionBindingType.Singleton:
                    _singletonInstances[type] = o;
                    break;

                case InjectionBindingType.Scoped:
                    scopedInstances[type] = o;
                    break;

                case InjectionBindingType.Transient:
                    break;

                default:
                    throw new Exception($"Unknown injection binding type for {type.FullName}");
            }
        }

        private object GetExistingInstanceOfTypeForScope(Type type, Dictionary<Type, object> scopedInstances, InjectionBindingType injectionBindingType)
        {
            switch (injectionBindingType)
            {
                case InjectionBindingType.Singleton:
                    if (_singletonInstances.ContainsKey(type))
                    {
                        return _singletonInstances[type];
                    }
                    return null;
                case InjectionBindingType.Scoped:
                    if (scopedInstances.ContainsKey(type))
                    {
                        return scopedInstances[type];
                    }
                    return null;

                case InjectionBindingType.Transient:
                    return null;

                default:
                    throw new Exception($"Unknown injection binding type for {type.FullName}");
            }
        }
    }
}