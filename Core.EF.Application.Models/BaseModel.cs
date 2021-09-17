using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.EF.Application.Models
{
    /// <summary>
    /// 基础模型
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [Key]
        [Column("ID")]
        public Guid Id { set; get; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("CREATETIME")]
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 是否删除--软删除
        /// </summary>
        [Required]
        [DefaultValue(false)]
        [Column("REMOVED")]
        public bool Removed { set; get; }

        public BaseModel()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }
    }
}
