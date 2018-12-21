using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Common
{
    public class BaseCustomException : Exception
    {
        public BaseCustomException()
            : base()
        {
        }

        public BaseCustomException(string message)
            : base(message)
        {
        }

        public BaseCustomException(string formate, params string[] message)
            : base(string.Format(formate, message))
        {
        }

        public BaseCustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public BaseCustomException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
    }

    public class LoginFaildException : Exception
    {
        public LoginFaildException()
            : base()
        {
        }

        public LoginFaildException(string message)
            : base(message)
        {
        }

        public LoginFaildException(string formate, params string[] message)
            : base(string.Format(formate, message))
        {
        }

        public LoginFaildException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public LoginFaildException(Exception innerException)
            : base(innerException.Message, innerException)
        {
        }
    }
}
