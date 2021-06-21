using System;
using System.Collections.Generic;
using System.Text;

namespace Lxsh.Project.EventBusDemo
{
    public class TestEventBus
    {
        public void TestEventBusRegister()
        {
            EventBus.Instance.Register(this);
            EventBus.Instance.Publish<EvOnLoginSuccess>(new EvOnLoginSuccess(new LoginResultDto { Id = "321", UserType ="1", Success = true }));
            Console.WriteLine("完成");
        }

        [Subscribe(ThreadMode = EventThreadMode.MAIN)]
        public void OnLoginSuccess(EvOnLoginSuccess ev)
        {
            if (ev.Info != null && !string.IsNullOrWhiteSpace(ev.Info?.UserType))
            {
                Console.WriteLine(ev.Info.Id);
            }
        }

        [Subscribe(ThreadMode = EventThreadMode.MAIN)]
        public void OnLoginSuccess1(EvOnLoginSuccess ev)
        {
            if (ev.Info != null && !string.IsNullOrWhiteSpace(ev.Info?.UserType))
            {
                Console.WriteLine(ev.Info.Success);
            }
        }
        [Subscribe(ThreadMode = EventThreadMode.MAIN)]
        public void OnLoginSuccess2(EvOnLoginSuccess ev)
        {
            if (ev.Info != null && !string.IsNullOrWhiteSpace(ev.Info?.UserType))
            {
                Console.WriteLine(ev.Info.UserType);
            }
        }
        [Subscribe(ThreadMode = EventThreadMode.MAIN)]
        public void OnLoginSuccess3(EvOnLoginSuccess ev)
        {
            if (ev.Info != null && !string.IsNullOrWhiteSpace(ev.Info?.UserType))
            {
                Console.WriteLine(ev.Info);
            }
        }
    }
}
