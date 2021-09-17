using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.EF.Application.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    [Table("USERINFOS")]
    public class UserInfo : BaseModel
    {
        /// <summary>
        /// 用户编码
        /// </summary>
        [Required]
        [MaxLength(20)]
        [Column("USERCDE")]
        public String UserCode { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        [MaxLength(100)]
        [Column("PASSWORD")]
        public string PassWord { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Column("USERNAME")]
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(100)]
        [Column("EMAIL")]
        public string Email { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Column("ISENABLED")]
        [DefaultValue(true)]
        public bool IsEnabled { get; set; }
    }
}
