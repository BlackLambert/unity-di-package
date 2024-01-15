using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public interface InstantiationInfo
    {
        InstanceCreationMode CreationMode { get; set; }
        Func<object> ProvideInstanceFunction { get; set; }
        bool InjectionAllowed { get; set; }
        Dictionary<BindingKey, object> Arguments { get; }
    }
}
