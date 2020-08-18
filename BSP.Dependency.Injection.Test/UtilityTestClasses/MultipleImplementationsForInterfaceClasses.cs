using BSP.Dependency.Injection.DependencyInjection;
using Moq;
using Xunit;

namespace BSP.Dependency.Injection.Test.UtilityTestClasses
{
    public interface TestingInterface
    {
        void CallWithName(string name);
    }

    public interface IMultipleImplementations
    {
        void DoThing(TestingInterface i);
    }

    public class Implementation1 : IMultipleImplementations
    {
        public void DoThing(TestingInterface i)
        {
            i.CallWithName("1");
        }
    }

    public class Implementation2 : IMultipleImplementations
    {
        public void DoThing(TestingInterface i)
        {
            i.CallWithName("2");
        }
    }

    public interface IMultipleImplementationConsumer
    {
        void DoThing(TestingInterface i);
    }
    public class MultipleImplementationConsumer : IMultipleImplementationConsumer
    {
        private readonly IMultipleImplementations[] _implementations;

        public MultipleImplementationConsumer(IMultipleImplementations[] implementations)
        {
            _implementations = implementations;
        }

        public void DoThing(TestingInterface i)
        {
            i.CallWithName("m");

            foreach (var imp in _implementations)
            {
                imp.DoThing(i);
            }
        }
    }

    public class MultipleImplementationsForInterfaceClassesTest
    {
        [Fact]
        public void Should_InjectBothImplementations()
        {
            var mockInterfaceImplementation = new Mock<TestingInterface>();

            var serviceMappingStore = new ServiceMappingStore();
            serviceMappingStore.AddTransient<IMultipleImplementations, Implementation1>();
            serviceMappingStore.AddTransient<IMultipleImplementations, Implementation2>();
            serviceMappingStore.AddTransient<IMultipleImplementationConsumer, MultipleImplementationConsumer>();
            serviceMappingStore.IntegrityCheck();

            var scope = new Scope(serviceMappingStore);

            var instance = scope.GetInstance<IMultipleImplementationConsumer>();

            instance.DoThing(mockInterfaceImplementation.Object);

            mockInterfaceImplementation.Verify(m => m.CallWithName("m"));
            mockInterfaceImplementation.Verify(m => m.CallWithName("1"));
            mockInterfaceImplementation.Verify(m => m.CallWithName("2"));
        }
    }
}