using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EF.Application.Dto
{
    public class BaseModelDto
    {
        public BaseModelDto()
        {
            CreateTime = DateTime.Now;
            if (Id == null || Id == Guid.Empty)
            {
                Id = Guid.NewGuid();
            }
        }

        public Guid Id { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
