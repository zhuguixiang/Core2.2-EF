using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.EF.Application.Dto
{
    public interface IDtoMapper
    {
        void CreateMappings(IMapperConfigurationExpression configurationExpression);
    }
}
