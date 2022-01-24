using System;
using BenchmarkDotNet.Running;

namespace Lxsh.Project.ReflectDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var sumary = BenchmarkRunner.Run<ReflectionBenchmarks>();
        }
    }
}
