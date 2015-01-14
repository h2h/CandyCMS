using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Framework.Caching
{
    public static class CacheExtensions
    {
        private static readonly object _syncObject = new object();
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            lock (_syncObject)
            {
                /*
                 * 是否已经缓存，如果已缓存就获取缓存中数据
                 * 没有就获取数据，加入缓存
                 */
                if (cacheManager.IsSet(key))
                    return cacheManager.Get<T>(key);

                var result = acquire();
                if (cacheTime > 0)
                    cacheManager.Set(key, result, cacheTime);

                return result;
            }
        }
    }
}
