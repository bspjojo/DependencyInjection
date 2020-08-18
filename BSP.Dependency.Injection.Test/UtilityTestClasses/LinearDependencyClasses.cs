namespace BSP.Dependency.Injection.Test.UtilityTestClasses
{
    public interface ILinearDependency1 { }
    public class LinearDependency1 : ILinearDependency1 { }

    public interface ILinearDependency2 { }
    public class LinearDependency2 : ILinearDependency2
    {
        public LinearDependency2(ILinearDependency1 l1) { }
    }

    public interface ILinearDependency3 { }
    public class LinearDependency3 : ILinearDependency3
    {
        public LinearDependency3(ILinearDependency2 l2) { }
    }
}