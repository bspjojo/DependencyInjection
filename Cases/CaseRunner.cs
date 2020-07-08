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
            serviceMappingStore.AddScoped<IDepInterface2, DepClass2>();
            serviceMappingStore.AddTransient<IDepInterface3, DepClass3>();

            var scope = new Scope(serviceMappingStore);

            var instance = scope.GetInstance<IDepInterface3>();
            instance.DoThing();
        }
    }
}