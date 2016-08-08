using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public interface IFoo { }

    public interface IBar { }

    public interface IBaz { }

    public interface IQux { }


    public class Foo : IFoo
    {
        public IBar Bar { get; private set; }

        [Injection]
        public IBaz Baz { get; set; }

        public Foo(IBar bar)
        {
            this.Bar = bar;
        }
    }

    [AttributeUsage(AttributeTargets.Constructor|
                    AttributeTargets.Property|
                    AttributeTargets.Method, 
                    AllowMultiple = false)]
    public class InjectionAttribute : Attribute{}

    public class Bar : IBar { }

    public class Baz : IBaz
    {

        public IQux Qux { get; private set; }

        public void Initialize(IQux qux)
        {
            this.Qux = qux;
        }
    }

    public class Qux : IQux { }


    public class Cat
    {
        private ConcurrentDictionary<Type, Type> typeMapping = new ConcurrentDictionary<Type, Type>();

        internal void Register<T1, T2>()
        {
            Register(typeof(T1), typeof(T2));
        }

        public void Register(Type from, Type to)
        {
            typeMapping[from] = to;
        }

        public T GetService<T>() where T : class
        {
            return this.GetService(typeof(T)) as T;
        }

        protected virtual ConstructorInfo GetConstructor(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            return constructors.FirstOrDefault(c => c.GetCustomAttribute<InjectionAttribute>() != null) ?? constructors.FirstOrDefault();
        }

        protected virtual void InitializeInjectedProperties(object service)
        {
            PropertyInfo[] properties = service.GetType().GetProperties()
                                        .Where(p => p.CanWrite && p.GetCustomAttribute<InjectionAttribute>() != null)
                                        .ToArray();
            Array.ForEach(properties, p => p.SetValue(service, this.GetService(p.PropertyType)));
        }

        protected virtual void InvokeInjectedMethods(object service)
        {
            MethodInfo[] methods = service.GetType().GetMethods()
                                   .Where(m => m.GetCustomAttribute<InjectionAttribute>() != null)
                                   .ToArray();
            Array.ForEach(methods, m =>
             {
                 object[] arguments = m.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
                 m.Invoke(service, arguments);
             });
        }


        public object GetService(Type serviceType)
        {
            Type type;

            if (!typeMapping.TryGetValue(serviceType, out type))
            {
                type = serviceType;
            }
            if (type.IsInterface || type.IsAbstract)
            {
                return null;
            }

            ConstructorInfo constructor = this.GetConstructor(type);
            if (null == constructor)
            {
                return null;
            }

            object[] arguments = constructor.GetParameters().Select(p => this.GetService(p.ParameterType)).ToArray();
            object service = constructor.Invoke(arguments);
            this.InitializeInjectedProperties(service);
            this.InvokeInjectedMethods(service);
            return service;
        }
    }
}
