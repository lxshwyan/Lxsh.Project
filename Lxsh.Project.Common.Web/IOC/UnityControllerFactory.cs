using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace Lxsh.Project.Common.Web.IOC
{
    /// <summary>
    /// 自定义的控制器实例化工厂
    /// </summary>
    public class UnityControllerFactory : DefaultControllerFactory
    {
        private IUnityContainer UnityContainer
        {
            get
            {
                return DIFactory.GetContainer();
            }
        }

        /// <summary>
        /// 创建控制器对象
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="controllerType"></param>
        /// <returns></returns>
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (null == controllerType)
            {
                return null;
            }
            IController controller = (IController)this.UnityContainer.Resolve(controllerType);
            return controller;
        }
        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="controller"></param>
        public override void ReleaseController(IController controller)
        {
            //释放对象
            //this.UnityContainer.Teardown(controller);//释放对象 Unity容器释放对象只有单例那些，瞬时的是不存在释放管理的，直接.net框架自身会即时完成对象释放
            /*
             I wrote an article about using object lifetimes managers in Unity and their impact on disposing. 
             If you use default TransientLifetimeManager or PerResolveLifetimeManager the Unity will even don't track existence of your objects so it can't call Dispose. 
             The only lifetime managers which calls Dispose on resolved instances are ContainerControlledLifetimeManager (aka singleton) and HierarchicalLifetimeManager.
             The Dispose is called when the lifetime manager is disposed.
             */
           base.ReleaseController(controller);//
        }
    }
}
