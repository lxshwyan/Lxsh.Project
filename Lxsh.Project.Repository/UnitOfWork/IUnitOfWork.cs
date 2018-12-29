/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Repository.UnitOfWork
*文件名： IUnitOfWork
*创建人： Lxsh
*创建时间：2018/12/25 15:06:46
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/25 15:06:46
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Repository
{
   public interface IUnitOfWork :IDisposable
    {
        DbContext DbContext { get; set; }
         /// <summary>
         /// 提交所有更改
         /// </summary>
         /// <returns></returns>
        Task CommitAsync();
        #region Methods
        /// <summary>
        /// 将指定的聚合根标注为“新建”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterNew<T>(T obj)
            where T : class;
        /// <summary>
        /// 将指定的聚合根标注为“更改”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterModified<T>(T obj)
            where T : class;
        /// <summary>
        /// 将指定的聚合根标注为“删除”状态。
        /// </summary>
        /// <typeparam name="T">需要标注状态的聚合根类型。</typeparam>
        /// <param name="obj">需要标注状态的聚合根。</param>
        void RegisterDeleted<T>(T obj)
            where T : class;
        #endregion
    }
}