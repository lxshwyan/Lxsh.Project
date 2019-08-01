/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.DesignPattern_TemplateMethod
*文件名： Exam
*创建人： Lxsh
*创建时间：2018/12/20 17:13:50
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/20 17:13:50
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.DesignPattern_TemplateMethod
{
    public abstract class Repository<T>  where T:class
    {

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity)
        {
                //......
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(T entity)
        {
              //......
            return true;
        }
        /// <summary>
        ///  删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Delete(T entity)
        {
            //......
            return true;
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<T> Query(int id)
        {
            //......
            return new List<T>();
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        public virtual void BulkInsert<T>(List<T> entities)
        {
            //由子类实现
        }
    }
}