using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.ObjectPool;

namespace Lxsh.Project.TelDemo
{
  
    public class FoobarService
    {
        public static int _lastestId;
        public int Id { get; set; }
        public FoobarService() => Id = Interlocked.Increment(ref _lastestId);
        public string this[int index]
        {
            get { return "fdsf"; }
        }
    }
}
