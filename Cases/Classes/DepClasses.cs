using System;

namespace BSP.Dependency.Injection.Cases.Classes
{
    public interface IDepInterface1
    {
        void DoThing();
    }

    public class DepClass1 : IDepInterface1
    {
        private readonly string _id;
        public DepClass1()
        {
            _id = Guid.NewGuid().ToString();
        }

        public void DoThing()
        {
            Console.WriteLine($"{nameof(DepClass1)}.{nameof(DoThing)} - {_id}");
        }
    }


    public interface IDepInterface2
    {
        void DoThing();
    }

    public class DepClass2 : IDepInterface2
    {
        private readonly IDepInterface1 _depInterface1;
        private readonly string _id;

        public DepClass2(IDepInterface1 depInterface1)
        {
            _depInterface1 = depInterface1;
            _id = Guid.NewGuid().ToString();
        }

        public void DoThing()
        {
            Console.WriteLine($"{nameof(DepClass2)}.{nameof(DoThing)} - {_id}");
            _depInterface1.DoThing();
        }
    }


    public interface IDepInterface3
    {
        void DoThing();
    }

    public class DepClass3 : IDepInterface3
    {
        private readonly IDepInterface2 _depInterface2;
        private readonly string _id;

        public DepClass3(IDepInterface2 depInterface2)
        {
            _depInterface2 = depInterface2;
            _id = Guid.NewGuid().ToString();
        }

        public void DoThing()
        {
            Console.WriteLine($"{nameof(DepClass3)}.{nameof(DoThing)} - {_id}");
            _depInterface2.DoThing();
        }
    }
}