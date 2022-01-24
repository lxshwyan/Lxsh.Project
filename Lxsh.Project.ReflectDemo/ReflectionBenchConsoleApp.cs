using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace Lxsh.Project.ReflectDemo
{
   public class ReflectionBenchmarks
    {
        
            private readonly ConstructorInfo _ctor;
            private readonly IServiceProvider _provider;
            private readonly Func<Employee> _expressionActivator;
            private readonly Func<Employee> _emitActivator;
            private readonly Func<Employee> _natashaActivator;
        public ReflectionBenchmarks()
        {
            _ctor = typeof(Employee).GetConstructor(Type.EmptyTypes);
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<Employee>();
            _provider = services.BuildServiceProvider();
           
        

            _expressionActivator = Expression.Lambda<Func<Employee>>(Expression.New(typeof(Employee))).Compile();

            DynamicMethod dynamic = new("DynamicMethod", typeof(Employee), null, typeof(ReflectionBenchmarks).Module, false);
            ILGenerator il = dynamic.GetILGenerator();
            il.Emit(OpCodes.Newobj, typeof(Employee).GetConstructor(System.Type.EmptyTypes));
            il.Emit(OpCodes.Ret);
            _emitActivator = dynamic.CreateDelegate(typeof(Func<Employee>)) as Func<Employee>;

        }
         [Benchmark(Baseline = true)]
         public Employee UseNew() => new Employee();
        [Benchmark]
        public Employee UseReflection() => _ctor.Invoke(null) as Employee;

        [Benchmark]
        public Employee UseActivator() => Activator.CreateInstance<Employee>();

        [Benchmark]
        public Employee UseDependencyInjection() => _provider.GetRequiredService<Employee>();
    }
    public class Employee {
        public int MyProperty1 { get; set; }
        public int MyProperty2{ get; set; }
        public int MyProperty3 { get; set; }
        public int MyProperty4 { get; set; }
        public string  MyProperty5 { get; set; }
        public string MyProperty6 { get; set; }
        public string MyProperty7 { get; set; }
        public string MyProperty8 { get; set; }
        public string MyProperty9 { get; set; }
    }
}
