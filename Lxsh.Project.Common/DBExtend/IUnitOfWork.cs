using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common.DBExtend
{
    /// <summary>
    /// 实现多DbContext的事务，支持分布式事务
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
    }
}
