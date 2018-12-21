using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common
{
    public static class ExtensionException
    {
        public static Exception GetInnestException(this Exception ex)
        {
            Exception innerException = ex.InnerException;
            Exception result = ex;
            while (innerException != null)
            {
                result = innerException;
                innerException = innerException.InnerException;
            }
            return result;
        }
       
    }
}
