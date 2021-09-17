using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Core.EF.Application.Commom
{
    public class FieldAccessorCache : FastReflectionCache<FieldInfo, IFieldAccessor>
    {
        protected override IFieldAccessor Create(FieldInfo key)
        {
            return FastReflectionFactories.FieldAccessorFactory.Create(key);
        }
    }
}
