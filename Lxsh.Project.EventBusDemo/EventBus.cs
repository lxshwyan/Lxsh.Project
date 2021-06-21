using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.EventBusDemo
{
    /// <summary>
    /// 事件总线
    /// </summary>
  public  class EventBus
    {
        public static EventBus _eventBus = null;
        private readonly object Lock_EventHandler = new object();
        private readonly object Lock_EventDelegates = new object();
        /// <summary>
        /// 领域模型事件句柄字典，用于存储领域模型的句柄
        /// </summary>
        private static Dictionary<Type, List<EvAttrHandler>> _dicEventHandler = new Dictionary<Type, List<EvAttrHandler>>();
        private static Dictionary<Type, List<Delegate>> _dicEventDelegates = new Dictionary<Type, List<Delegate>>();
        public static EventBus Instance
        {
            get
            {
                return _eventBus ?? (_eventBus = new EventBus());
            }
        }
        private readonly Func<EvAttrHandler, object, bool> eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.Handler.GetType();
            var o2Type = o2.GetType();
            return o1Type == o2Type;
        };
        public void Register(object callInstance)
        {
            var regType = callInstance.GetType();
            var methodArray = regType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            if (methodArray == null || methodArray.Length < 1)
                throw new Exception("未找到Public的实例方法从" + regType.ToString());


            //Method Check
            var dicMethods = new System.Collections.Generic.Dictionary<MethodInfo, SubscribeAttribute>();
            foreach (var method in methodArray)
            {
                var attr = method.GetCustomAttribute<SubscribeAttribute>(false);
                if (attr == null)
                    continue;

                var parameters = method.GetParameters();
                if (parameters == null || parameters.Length != 1)
                    throw new Exception(string.Format("订阅方法{0}必须只包含一个参数", method.Name));

                var returnType = method.ReturnType;
                if (returnType != typeof(void))
                    throw new Exception(string.Format("订阅方法{0}不能有返回值", method.Name));

                dicMethods.Add(method, attr);
            }

            if (dicMethods == null || dicMethods.Count < 1)
                throw new Exception("未找到Public声明的具有Subscribe特性的实例方法从" + regType.ToString());

            var invokeMemberBase = this.GetType().GetMethod("SubscribeAction", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var method in dicMethods.Keys)
            {
                var attr = dicMethods[method];//特性
                var evType = method.GetParameters()[0].ParameterType;//
                var invoker = invokeMemberBase.MakeGenericMethod(new Type[] { evType });

                var firstParType = method.GetParameters()[0].ParameterType;
                var delegateType = typeof(Action<>);
                delegateType = delegateType.MakeGenericType(firstParType);

                var p1Delegate = Delegate.CreateDelegate(delegateType, callInstance, method, true);
                invoker.Invoke(this, BindingFlags.Default, null, new object[] { p1Delegate, attr }, null);
                lock (Lock_EventDelegates)
                {
                    if (_dicEventDelegates.ContainsKey(regType))
                    {
                        var handlers = _dicEventDelegates[regType];
                        if (handlers != null)
                        {
                            handlers.Add(p1Delegate);
                        }
                        else
                        {
                            handlers = new List<Delegate> { p1Delegate };
                        }
                    }
                    else
                    {
                        _dicEventDelegates.Add(regType, new List<Delegate> { p1Delegate });
                    }
                }
            }
        }

        public void UnRegister(object callInstance)
        {
            var regType = callInstance.GetType();
            var listDelegate = new List<Delegate>();

            lock (Lock_EventDelegates)
            {
                if (_dicEventDelegates.ContainsKey(regType))
                {
                    listDelegate = _dicEventDelegates[regType];
                    _dicEventDelegates.Remove(regType);
                }
            }

            var invokeMemberBase = this.GetType().GetMethod("UnsubscribeAction", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);

            foreach (var dele in listDelegate)
            {
                var firstArgType = dele.Method.GetParameters()[0].ParameterType;
                var invoker = invokeMemberBase.MakeGenericMethod(new Type[] { firstArgType });
                invoker.Invoke(this, BindingFlags.Default, null, new object[] { dele }, null);
            }
        }
        #region 发布事件
        public void Publish<TEvent>(TEvent tEvent) where TEvent : IBusEvent
        {
            var eventType = typeof(TEvent);
            if (_dicEventHandler.ContainsKey(eventType) && _dicEventHandler[eventType] != null &&
                _dicEventHandler[eventType].Count > 0)
            {
                var handlers = _dicEventHandler[eventType];
                foreach (var handler in handlers)
                {
                    var attrHandler = handler as EvAttrHandler;
                    var eventHandler = attrHandler.Handler as IEventHandler<TEvent>;
                    var eventAttr = attrHandler.Attr;
                    Task.Run(() =>
                    {
                        eventHandler.Handle(tEvent);
                    });
                    //switch (eventAttr.ThreadMode)
                    //{
                    //    case EventThreadMode.MAIN:
                    //        Xamarin.Essentials.MainThread.BeginInvokeOnMainThread(() =>
                    //        {
                    //            eventHandler.Handle(tEvent);
                    //        });
                    //        break;
                    //    case EventThreadMode.BACKGROUND:
                    //        if (Xamarin.Essentials.MainThread.IsMainThread)
                    //        {
                    //            Task.Run(() =>
                    //            {
                    //                eventHandler.Handle(tEvent);
                    //            });
                    //        }
                    //        else
                    //        {
                    //            eventHandler.Handle(tEvent);
                    //        }
                    //        break;
                    //    case EventThreadMode.ASYNC:
                    //        Task.Run(() =>
                    //        {
                    //            eventHandler.Handle(tEvent);
                    //        });
                    //        break;
                    //    default:
                    //    case EventThreadMode.POSTING:
                    //        eventHandler.Handle(tEvent);
                    //        break;

                    //}
                }
            }
        }
        #endregion

        #region private 订阅事件

        /// <summary>
        /// 订阅事件实体
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subTypeList"></param>
        private void SubscribeAction<TEvent>(Action<TEvent> eventHandlerFunc, SubscribeAttribute attr)
            where TEvent : IBusEvent
        {
            SubscribeHandler<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventHandlerFunc), attr);
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandler"></param>
        private void SubscribeHandler<TEvent>(IEventHandler<TEvent> eventHandler, SubscribeAttribute attr) where TEvent : IBusEvent
        {
            //同步锁
            lock (Lock_EventHandler)
            {
                //获取领域模型的类型
                var eventType = typeof(TEvent);
                //如果此领域类型在事件总线中已注册过
                if (_dicEventHandler.ContainsKey(eventType))
                {
                    var handlers = _dicEventHandler[eventType];
                    if (handlers != null)
                    {
                        handlers.Add(new EvAttrHandler(eventHandler, attr));
                    }
                    else
                    {
                        handlers = new List<EvAttrHandler> { new EvAttrHandler(eventHandler, attr) };
                    }
                }
                else
                {
                    _dicEventHandler.Add(eventType, new List<EvAttrHandler> { new EvAttrHandler(eventHandler, attr) });
                }
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subTypeList"></param>
        private void UnsubscribeAction<TEvent>(Action<TEvent> eventHandlerFunc)
            where TEvent : IBusEvent
        {
            UnsubscribeHandler<TEvent>(new ActionDelegatedEventHandler<TEvent>(eventHandlerFunc));
        }


        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subType"></param>
        private void UnsubscribeHandler<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IBusEvent
        {
            lock (Lock_EventHandler)
            {
                var eventType = typeof(TEvent);
                if (_dicEventHandler.ContainsKey(eventType))
                {
                    var handlers = _dicEventHandler[eventType];

                    if (handlers != null
                        && handlers.Exists(deh => eventHandlerEquals(deh, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(deh => eventHandlerEquals(deh, eventHandler));
                        handlers.Remove(handlerToRemove);
                        //var equesHandlers = handlers.Where(deh => eventHandlerEquals(deh, eventHandler));
                        //if (equesHandlers != null)
                        //{
                        //    foreach (var item in equesHandlers)
                        //    {
                        //        handlers.Remove(item);
                        //    }
                        //}
                    }
                }
            }
        }
        #endregion

    }
  

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class SubscribeAttribute : Attribute
    {
        public EventThreadMode ThreadMode { get; set; } = EventThreadMode.POSTING;
        public EventThreadMode ThreadMode1 { get; set; } = EventThreadMode.POSTING;
    }
    public enum EventThreadMode
    {
        /// <summary>
        /// POSTING：默认，表示事件处理函数的线程跟发布事件的线程在同一个线程。
        /// </summary>
        POSTING,
        /// <summary>
        /// MAIN：表示事件处理函数的线程在主线程(UI)线程，因此在这里不能进行耗时操作。
        /// </summary>
        MAIN,
        /// <summary>
        /// BACKGROUND：表示事件处理函数的线程在后台线程，因此不能进行UI操作。如果发布事件的线程是主线程(UI线程)，那么事件处理函数将会开启一个后台线程，如果果发布事件的线程是在后台线程，那么事件处理函数就使用该线程。
        /// </summary>
        BACKGROUND,
        /// <summary>
        /// ASYNC：表示无论事件发布的线程是哪一个，事件处理函数始终会新建一个子线程运行，同样不能进行UI操作。
        /// </summary>
        ASYNC
    }



    public class EvAttrHandler
    {
        public EvAttrHandler(object handler, SubscribeAttribute attr)
        {
            this.Handler = handler;
            this.Attr = attr;
        }

        public object Handler { get; set; }

        public SubscribeAttribute Attr { get; set; }
    }

    /// <summary>
    /// 标记接口
    /// </summary>
    public class IBusEvent
    {
    }
    public sealed class ActionDelegatedEventHandler<TEvent> : IEventHandler<TEvent>
     where TEvent : IBusEvent
    {
        #region Private Fields
        private readonly Action<TEvent> action;
        #endregion

        #region Ctor
        /// <summary>

        /// Initializes a new instance of <c>ActionDelegatedEventHandler{TEvent}</c> class.

        /// </summary>

        /// <param name="action">The <see cref="Action{T}"/> instance that delegates the event handling process.</param>

        public ActionDelegatedEventHandler(Action<TEvent> action)
        {
            this.action = action;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a <see cref="Boolean"/> value which indicates whether the current
        /// <c>ActionDelegatedEventHandler{T}</c> equals to the given object.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> which is used to compare to
        /// the current <c>ActionDelegatedEventHandler{T}</c> instance.</param>
        /// <returns>If the given object equals to the current <c>ActionDelegatedEventHandler{T}</c>
        /// instance, returns true, otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj == null)
                return false;
            ActionDelegatedEventHandler<TEvent> other = obj as ActionDelegatedEventHandler<TEvent>;
            if (other == null)
                return false;
            return Delegate.Equals(this.action, other.action);
        }

        #endregion

        #region IHandler<TDomainEvent> Members
        /// <summary>
        /// Handles the specified message.
        /// </summary>
        /// <param name="message">The message to be handled.</param>
        public void Handle(TEvent message)
        {
            action(message);
        }

        #endregion
    }
    /// <summary>
    /// 事件处理接口
    /// </summary>
    /// <typeparam name="TEvent">继承IEvent对象的事件源对象</typeparam>
    public interface IEventHandler<TEvent> where TEvent : IBusEvent
    {
        /// <summary>
        /// 处理程序
        /// </summary>
        /// <param name="evt"></param>
        void Handle(TEvent evt);

    }

}
