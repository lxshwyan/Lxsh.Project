/************************************************************************
* Copyright (c) 2018 All Rights Reserved.
*命名空间：Lxsh.Project.Repository.UnitOfWork
*文件名： UnitOfWork
*创建人： Lxsh
*创建时间：2018/12/25 15:12:24
*描述
*=======================================================================
*修改标记
*修改时间：2018/12/25 15:12:24
*修改人：Lxsh
*描述：
************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext DbContext { get; set; }
        public UnitOfWork(DbContext contextHelp)
        {
            this.DbContext = DbContext;
        }

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public virtual async Task CommitAsync()
        {
            // Save changes with the default options
            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }

        }

        /// <summary>
        /// Disposes the current object
        /// </summary>
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes all external resources.
        /// </summary>
        /// <param name="disposing">The dispose indicator.</param>
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (DbContext == null) return;

            DbContext.Dispose();
            DbContext = null;
        }

        public virtual void RegisterNew<TEntity>(TEntity obj) where TEntity : class
        {
            DbContext.Set<TEntity>().Add(obj);
        }

        public virtual void RegisterModified<TEntity>(TEntity obj) where TEntity : class
        {
            DbContext.Entry(obj).State = EntityState.Modified;
        }

        public virtual void RegisterDeleted<TEntity>(TEntity obj) where TEntity : class
        {
            DbContext.Entry(obj).State = EntityState.Deleted;
        }

    }
}