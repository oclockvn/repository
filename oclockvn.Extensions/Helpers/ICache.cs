using System;

namespace oclockvn.Extensions.Helpers
{
    public interface ICache
    {
        /// <summary>
        /// get data from cache. If not exist, fetch from data source
        /// </summary>
        /// <typeparam name="T">return data</typeparam>
        /// <param name="key">string of key to get or set</param>
        /// <param name="factory">callback used to get data if not exist</param>
        /// <param name="expire">expiration time, in minute</param>
        /// <returns></returns>
        T GetOrSet<T>(string key, Func<T> factory, int expire);

        /// <summary>
        /// remote cache
        /// </summary>
        /// <param name="key">string of key to remove</param>        
        void Remove(string key);
    }
}
