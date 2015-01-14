
namespace Candy.Framework.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// 获取或设置指定键缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// 移除指定缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
        /// <summary>
        /// 清除所有缓存值
        /// </summary>
        void Clear();
        /// <summary>
        /// 指定 key 是否缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsSet(string key);
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        void RemoveByPattern(string pattern);
    }
}
