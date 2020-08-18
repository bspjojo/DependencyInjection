namespace BSP.Dependency.Injection.Test.UtilityTestClasses
{
    public interface ICircularDependency1 { }

    public class CircularDependency1 : ICircularDependency1
    {
        public CircularDependency1(ICircularDependency2 c2) { }
    }

    public interface ICircularDependency2 { }

    public class CircularDependency2 : ICircularDependency2
    {
        public CircularDependency2(ICircularDependency1 c1) { }
    }
}