/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_Obsever
*文件名： Subject
*创建人： Lxsh
*创建时间：2018/12/21 17:09:46
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/21 17:09:46
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_Obsever
{
    public class Subject : ISubject
    {
        List<IObserver> observerList = new List<IObserver>();
        public string SubjectState { get; set; }

        public void Add(IObserver observer)
        {
            observerList.Add(observer);
        }

        public void Nofity()
        {
            foreach (var observer in observerList)
            {
                observer.Modidy(SubjectState);
            }
        }

        public void Remove(IObserver observer)
        {
            observerList.Remove(observer);
        }
    }
}