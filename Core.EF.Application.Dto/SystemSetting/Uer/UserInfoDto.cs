using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EF.Application.Dto
{
    /// <summary>
    /// 用户信息Dto
    /// </summary>
    public class UserInfoDto
    {
        /// <summary>
        /// id
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 用户编码
        /// </summary>
        public String UserCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string PassWord { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsEnabled { get; set; }
    }
}
