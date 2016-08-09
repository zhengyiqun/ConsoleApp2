using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Cat cat = new Cat();

            //注册
            cat.Register<IFoo, Foo>();
            cat.Register<IBar, Bar>();
            cat.Register<IBaz, Baz>();
            cat.Register<IQux, Qux>();

            IFoo service = cat.GetService<IFoo>();
            Foo foo = (Foo)service;
            Baz baz = (Baz)foo.Baz;

            Console.WriteLine("cat.GetService<IFoo>(): {0}", service);
            Console.WriteLine("cat.GetService<IFoo>().Bar: {0}", foo.Bar);
            Console.WriteLine("cat.GetService<IFoo>().Baz: {0}", foo.Baz);
            Console.WriteLine("cat.GetService<IFoo>().Baz.Qux: {0}", baz.Qux);
        }
    }
}
