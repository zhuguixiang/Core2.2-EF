using AutoMapper;
using Core.EF.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EF.Application.Dto.SystemSetting
{
    public class UserMapper : IDtoMapper
    {
        public void CreateMappings(IMapperConfigurationExpression x)
        {
            x.CreateMap<UserInfo, UserInfoDto>();
        }
    }
}
