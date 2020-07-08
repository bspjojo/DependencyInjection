using System;
using BSP.Dependency.Injection.Cases;

namespace BSP.Dependency.Injection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var c = new CaseRunner();
            c.Run();

            Console.ReadLine();
        }
    }
}
