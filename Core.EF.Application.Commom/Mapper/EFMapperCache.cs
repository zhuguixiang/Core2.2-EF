using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Core.EF.Application.Commom.Core
{
    static class EFMapperCache
    {
        private static Dictionary<Type, EFMapperTypeDescription> _cacheList = new Dictionary<Type, EFMapperTypeDescription>();

        static EFMapperCache()
        {

        }

        public static EFMapperTypeDescription Get(Type type)
        {
            if (type == null)
                return null;

            if (_cacheList.Keys.Contains(type))
                return _cacheList[type];
            else
            {
                EFMapperTypeDescription cacheCodon = new EFMapperTypeDescription(type);

                Monitor.Enter(_cacheList);

                if (_cacheList.Keys.Contains(type) == false)
                    _cacheList.Add(type, cacheCodon);

                Monitor.Exit(_cacheList);

                return cacheCodon;

            }
        }

    }
}
