using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.EventBusDemo
{
 
    public class TestEventBus1
    {
        public TestEventBus1()
        {
            EventBus.Instance.Register(this);
        }
        [Subscribe(ThreadMode = EventThreadMode.MAIN)]
        public void OnLoginSuccess(EvOnLoginSuccess ev)
        {
            if (ev.Info != null && !string.IsNullOrWhiteSpace(ev.Info?.UserType))
            {
                Console.WriteLine(this.GetType().FullName);
            }
        }
    }
    public class TestEventBus2
    {
      
        public TestEventBus2()
        {
            EventBus.Instance.Register(this);
        }
        [Subscribe(ThreadMode = EventThreadMode.MAIN)]
        public void OnLoginSuccess(EvOnLoginSuccess ev)
        {
            if (ev.Info != null && !string.IsNullOrWhiteSpace(ev.Info?.UserType))
            {
                Console.WriteLine(this.GetType().FullName);
            }
        }
    }
}
