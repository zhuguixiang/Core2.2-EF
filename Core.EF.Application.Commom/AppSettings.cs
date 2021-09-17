using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EF.Application.Commom
{
    public class AppSettings
    {
        public AppSettings_RedisRedisCache RedisCache { set; get; }
    }

    /// <summary>
    /// redis
    /// </summary>
    public class AppSettings_RedisRedisCache
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 链接信息
        /// </summary>
        public string ConnectionString { get; set; }
    }
}
