using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Core.EF.Application.Commom
{
    public class ConstructorInvokerCache : FastReflectionCache<ConstructorInfo, IConstructorInvoker>
    {
        protected override IConstructorInvoker Create(ConstructorInfo key)
        {
            return FastReflectionFactories.ConstructorInvokerFactory.Create(key);
        }
    }
}
