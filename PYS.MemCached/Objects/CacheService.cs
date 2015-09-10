using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Enyim.Caching;
using Enyim.Caching.Memcached;


public static class CacheService
{
    private static readonly MemcachedClient client = new MemcachedClient();

    private static MemcachedClient Client
    {
        get
        {
            return client;
        }
    }

    public static object Get(string key)
    {

        try
        {
            object objCacheEntry = Client.Get(key);
            return objCacheEntry;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void Remove(string key)
    {
        try
        {
            Client.Remove(key);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void Set(string key, object val)
    {
        try
        {
            bool Successfull = Client.Store(StoreMode.Set, key, val);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public static void Set(string key, object val, int durationMinute)
    {
        try
        {
            bool Successfull = Client.Store(StoreMode.Set, key, val, new TimeSpan(0, durationMinute, 0));
        }

        catch (Exception)
        {
            throw;
        }
    }
}
