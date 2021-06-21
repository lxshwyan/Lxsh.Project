using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lxsh.Project.Thread.Demo
{
	public class CacheTimeHelper
    {
		public static Dictionary<string, DateTime> lastClearTimePerProduct = new Dictionary<string, DateTime>();

		private const int ENDPOINT_CACHE_TIME = 3600;

		public static bool CheckCacheIsExpire(string product, string regionId)
		{
			string key = product + "_" + regionId;
			DateTime dateTime;
			if (lastClearTimePerProduct.ContainsKey(key))
			{
				dateTime = lastClearTimePerProduct[key];
			}
			else
			{
				dateTime = DateTime.Now;
				lastClearTimePerProduct.Add(key, dateTime);
			}
			if (3600.0 < (DateTime.Now - dateTime).TotalSeconds)
			{
				return true;
			}
			return false;
		}

		public static void AddLastClearTimePerProduct(string product, string regionId, DateTime lastClearTime)
		{
			string key = product + "_" + regionId;
			if (lastClearTimePerProduct.ContainsKey(key))
			{
				lastClearTimePerProduct.Remove(key);
			}
			lastClearTimePerProduct.Add(key, lastClearTime);
		}
	}
}
