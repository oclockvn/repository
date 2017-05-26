using System;

namespace oclockvn.Extensions.Helpers
{
    public interface ICache
    {
        T GetOrSet<T>(string key, Func<T> factory, int expire);
    }
}
