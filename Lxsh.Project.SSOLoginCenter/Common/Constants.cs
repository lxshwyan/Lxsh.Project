using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lxsh.Project.SSOLoginCenter
{
    internal static class Constants
    {
        public const string CookieName = "UserAuthTicket";
        public const string UserName = "UserName";
        public const int ExpiresDay = 7;
        public const string ReturnUrl = "ReturnUrl";

        private static ICacheManager<object> _ICacheManager;
        public static ICacheManager<object> ICacheManager
        {
            get
            {
                if (_ICacheManager == null)
                    _ICacheManager = CacheFactory.Build("LoginCache", settings =>
                     {
                         settings.WithSystemRuntimeCacheHandle("handleName");
                     });
                return _ICacheManager;
            }
        }
    }
}