using AutoMapper;
using Core.EF.Application.Commom;
using Core.EF.Application.Dto;
using System;
using System.Collections.Generic;

namespace Core.EF.Application.Api
{
    public class AutoMapperConfig
    {
        private static bool _Initialized = false;

        public static void Initialize()
        {
            if (_Initialized)
                return;

            Mapper.Initialize(Configuration);

            _Initialized = true;

        }

        public static void Configuration(IMapperConfigurationExpression x)
        {
            List<Type> dtoMapperTypeList = ReflectionHelper.GetTypeListBaseOn<IDtoMapper>();

            foreach (var item in dtoMapperTypeList)
            {
                IDtoMapper dtoMapper = (IDtoMapper)Activator.CreateInstance(item);
                dtoMapper.CreateMappings(x);
            }
        }
    }
}
