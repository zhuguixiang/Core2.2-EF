using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EF.Application.Models
{
    /// <summary>
    /// 数据库实体--配置上下文
    /// </summary>
    public class EFCoreContex : DbContext
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }

        public EFCoreContex(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public static EFCoreContex CreateContext()
        {
            var optionBuilder = new DbContextOptionsBuilder<EFCoreContex>();
            //https://blog.csdn.net/xtjatswc/article/details/104013580
            optionBuilder.UseOracle(ConnectionString, b => b.UseOracleSQLCompatibility("11"));
            var context = new EFCoreContex(optionBuilder.Options);
            return context;
        }

        /// <summary>
        /// 用户信息表
        /// </summary>
        public DbSet<UserInfo> UserInfo { get; set; }
    }
}
