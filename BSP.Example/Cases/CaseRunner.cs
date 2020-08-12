using System;
using BSP.Dependency.Injection.DependencyInjection;
using BSP.Example.Cases.Classes;

namespace BSP.Example.Cases
{
    public class CaseRunner
    {
        public void Run()
        {
            var serviceMappingStore = new ServiceMappingStore();
            serviceMappingStore.AddSingleton<IDepInterface1, DepClass1>();
            serviceMappingStore.AddTransient<IDepInterface2, DepClass2>();
            serviceMappingStore.AddScoped<IDepInterface3, DepClass3>();
            serviceMappingStore.AddScoped<IDepInterface4, DepClass4>();
            serviceMappingStore.AddTransient<IDepInterface5, DepClass5>();

            // Circular dependency
            // serviceMappingStore.AddSingleton<ICircSelf, CircSelf>();
            // serviceMappingStore.AddTransient<ICircOther1, CircOther1>();
            // serviceMappingStore.AddScoped<ICircOther2, CircOther2>();

            serviceMappingStore.IntegrityCheck();

            var scope = new Scope(serviceMappingStore);

            var instance1 = scope.GetInstance<IDepInterface5>();
            instance1.DoThing();

            Console.WriteLine("new scope");

            var instance2 = scope.GetInstance<IDepInterface2>();
            instance2.DoThing();
        }
    }
}