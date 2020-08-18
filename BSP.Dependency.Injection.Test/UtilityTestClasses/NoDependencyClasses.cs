namespace BSP.Dependency.Injection.Test.UtilityTestClasses
{
    public interface INoDependencySingleton { }

    public class NoDependencySingleton : INoDependencySingleton { }

    public interface INoDependencyScoped { }

    public class NoDependencyScoped : INoDependencyScoped { }

    public interface INoDependencyTransient { }

    public class NoDependencyTransient : INoDependencyTransient { }
}