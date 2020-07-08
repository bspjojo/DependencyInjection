using System;
using System.Collections.Generic;
using System.Linq;

namespace BSP.Dependency.Injection.DependencyInjection
{
    public class Scope
    {
        private readonly ServiceMappingStore _serviceMappingStore;

        public Scope(ServiceMappingStore serviceMappingStore)
        {
            _serviceMappingStore = serviceMappingStore;
        }

        public T GetInstance<T>()
        {
            return (T)GetObjectInstance(typeof(T));
        }

        private object GetObjectInstance(Type t)
        {
            var tType = _serviceMappingStore.GetImplementationTypeForInterface(t);

            Console.WriteLine($"{t.FullName} to {tType.FullName}");

            var constructor = tType.GetConstructors().Single();
            var paramInfos = constructor.GetParameters();

            if (paramInfos.Length == 0)
            {
                Console.WriteLine($"Creating {tType.FullName}");
                return Activator.CreateInstance(tType);
            }

            var constructorArgs = new List<object>();

            foreach (var p in paramInfos)
            {
                Console.WriteLine(p.ParameterType.FullName);
                constructorArgs.Add(GetObjectInstance(p.ParameterType));
            }

            Console.WriteLine($"Creating {tType.FullName} with args");
            return Activator.CreateInstance(tType, constructorArgs.ToArray());
        }
    }
}