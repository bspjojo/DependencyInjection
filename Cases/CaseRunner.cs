using BSP.Dependency.Injection.DependencyInjection;
using BSP.Dependency.Injection.Cases.Classes;

namespace BSP.Dependency.Injection.Cases
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

            var scope = new Scope(serviceMappingStore);

            var instance = scope.GetInstance<IDepInterface5>();
            instance.DoThing();
        }
    }
}