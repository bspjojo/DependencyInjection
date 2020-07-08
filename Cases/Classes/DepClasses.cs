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
            Console.WriteLine("-----------------------------");
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
        private readonly IDepInterface1 _depInterface1;
        private readonly string _id;

        public DepClass3(IDepInterface2 depInterface2, IDepInterface1 depInterface1)
        {
            _depInterface2 = depInterface2;
            _depInterface1 = depInterface1;
            _id = Guid.NewGuid().ToString();
        }

        public void DoThing()
        {
            Console.WriteLine($"{nameof(DepClass3)}.{nameof(DoThing)} - {_id}");
            _depInterface2.DoThing();
            _depInterface1.DoThing();
        }
    }


    public interface IDepInterface4
    {
        void DoThing();
    }

    public class DepClass4 : IDepInterface4
    {
        private readonly IDepInterface3 _depInterface3;
        private readonly IDepInterface2 _depInterface2;
        private readonly IDepInterface1 _depInterface1;
        private readonly string _id;

        public DepClass4(IDepInterface3 depInterface3, IDepInterface2 depInterface2, IDepInterface1 depInterface1)
        {
            _depInterface3 = depInterface3;
            _depInterface2 = depInterface2;
            _depInterface1 = depInterface1;
            _id = Guid.NewGuid().ToString();
        }

        public void DoThing()
        {
            Console.WriteLine($"{nameof(DepClass4)}.{nameof(DoThing)} - {_id}");
            _depInterface3.DoThing();
            _depInterface2.DoThing();
            _depInterface1.DoThing();
        }
    }

    public interface IDepInterface5
    {
        void DoThing();
    }

    public class DepClass5 : IDepInterface5
    {
        private readonly IDepInterface4 _depInterface4;
        private readonly IDepInterface3 _depInterface3;
        private readonly IDepInterface2 _depInterface2;
        private readonly IDepInterface1 _depInterface1;
        private readonly string _id;

        public DepClass5(IDepInterface4 depInterface4, IDepInterface3 depInterface3, IDepInterface2 depInterface2, IDepInterface1 depInterface1)
        {
            _depInterface4 = depInterface4;
            _depInterface3 = depInterface3;
            _depInterface2 = depInterface2;
            _depInterface1 = depInterface1;
            _id = Guid.NewGuid().ToString();
        }

        public void DoThing()
        {
            Console.WriteLine($"{nameof(DepClass5)}.{nameof(DoThing)} - {_id}");
            _depInterface4.DoThing();
            _depInterface3.DoThing();
            _depInterface2.DoThing();
            _depInterface1.DoThing();
        }
    }
}