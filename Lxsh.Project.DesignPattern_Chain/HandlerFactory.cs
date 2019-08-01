/************************************************************************
* Copyright (c) 2019 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Chain
*文件名： HandlerFactory
*创建人： Lxsh
*创建时间：2019/7/20 10:23:14
*描述
*=======================================================================
*修改标记
*修改时间：2019/7/20 10:23:14
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Chain
{
   public class HandlerFactory
    {
        /// <summary>
        /// A=>B=>C
        /// </summary>
        /// <returns></returns>
        public static AbstractHander getABCHandler()
        {
            ConcreateHandleA concreateHandleA = new ConcreateHandleA();
            ConcreateHandleB concreateHandleB = new ConcreateHandleB();
            ConcreateHandleC concreateHandleC = new ConcreateHandleC();

            concreateHandleA.Hander = concreateHandleB;
            concreateHandleB.Hander = concreateHandleC;

            return concreateHandleA;
        }
        /// <summary>
        /// C=>B=>A
        /// </summary>
        /// <returns></returns>
        public static AbstractHander getCBAHandler()
        {
            ConcreateHandleA concreateHandleA = new ConcreateHandleA();
            ConcreateHandleB concreateHandleB = new ConcreateHandleB();
            ConcreateHandleC concreateHandleC = new ConcreateHandleC();

            concreateHandleC.Hander = concreateHandleB;
            concreateHandleB.Hander = concreateHandleA;

            return concreateHandleC;
        }
        /// <summary>
        /// A=>C
        /// </summary>
        /// <returns></returns>
        public static AbstractHander getACHandler()
        {
            ConcreateHandleA concreateHandleA = new ConcreateHandleA(); 
            ConcreateHandleC concreateHandleC = new ConcreateHandleC();

            concreateHandleA.Hander = concreateHandleC;       

            return concreateHandleA;
        }
        /// <summary>
        /// B=>C
        /// </summary>
        /// <returns></returns>
        public static AbstractHander getbCHandler()
        {
          
            ConcreateHandleB concreateHandleB = new ConcreateHandleB();
            ConcreateHandleC concreateHandleC = new ConcreateHandleC();
            concreateHandleB.Hander = concreateHandleC;
         

            return concreateHandleB;
        }
    }
}