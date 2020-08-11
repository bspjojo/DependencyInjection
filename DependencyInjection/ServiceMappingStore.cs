using System;
using System.Collections.Generic;
using System.Linq;

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

        public void IntegrityCheck()
        {
            foreach (var kvp in _bindings)
            {
                CheckIntegrityForRootType(kvp.Value);
            }
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

        private void CheckIntegrityForRootType(Type rootType)
        {
            var foundTypes = new Stack<Type>();

            Console.WriteLine($"Checking integrity for: {rootType.Name}");

            CheckIntegrityForType(rootType, foundTypes);
        }

        private void CheckIntegrityForType(Type typeToCheck, Stack<Type> foundTypes)
        {
            if (foundTypes.Contains(typeToCheck))
            {
                var tString = string.Join(" -> ", foundTypes.Select(v => v.Name));

                throw new Exception($"Circular reference detected. {typeToCheck.Name} -> {tString}");
            }

            foundTypes.Push(typeToCheck);

            var constructor = typeToCheck.GetConstructors().Single();
            var paramInfos = constructor.GetParameters();

            foreach (var p in paramInfos)
            {
                var found = _bindings.TryGetValue(p.ParameterType, out var classType);
                if (!found)
                {
                    throw new Exception($"{p.ParameterType.Name} mapping not added.");
                }

                CheckIntegrityForType(classType, foundTypes);
            }

            foundTypes.Pop();
        }
    }

    public enum InjectionBindingType
    {
        Singleton, // only ever one
        Scoped, // one instance per resolution
        Transient // new instance each time
    }
}