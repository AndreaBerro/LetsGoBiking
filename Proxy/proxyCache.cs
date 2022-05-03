using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;

namespace Proxy
{

    // Proxy Cache Générique
    public class ProxyCache<T> where T : class, new()
    {
        //set a l'infini pour la démo
        public DateTimeOffset dt_default = ObjectCache.InfiniteAbsoluteExpiration;
        ObjectCache cache = MemoryCache.Default;
        public ProxyCache()
        {
        }

        //Method qui permet d'aller chercher et/ou de mettre en cache un objet pendant un temps infini (utilisé en Demo)
        public T Get(string CacheItemName, List<string> argSupp = null)
        {
            var item = cache.Get(CacheItemName);
            if (item == null)
            {
                item = new T();
                cache.Set(CacheItemName, item, dt_default);
            }
            return (T)item;
        }

        //Method qui permet d'aller chercher et/ou de mettre en cache un objet pendant un temps défini
        public T Get(string CacheItemName, double dt_seconds, List<string> argSupp = null)
        {
            var item = cache.Get(CacheItemName);
            if (item == null)
            {
                item = new T();
                cache.Set(CacheItemName, item,new DateTimeOffset(DateTime.Now.AddSeconds(dt_seconds)));
            }
            return (T)item;
        }

        //Method qui permet d'aller chercher et/ou de mettre en cache un objet pendant un temps défini
        public T Get(string CacheItemName, DateTimeOffset dt, List<string> argSupp = null)
        {
            var item = cache.Get(CacheItemName);
            if (item == null)
            {
                item = new T();
                cache.Set(CacheItemName, item, dt);
            }
            return (T)item;
        }
    }
}