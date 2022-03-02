using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public interface InstantiationInfo
    {
        Type ConcreteType { get; set; }
        InstanceCreationMode CreationMode { get; set; }
        Func<object> ProvideInstanceFunction { get; set; }
        bool InjectionAllowed { get; set; }
        Dictionary<BindingKey, object> Arguments { get; }
    }
}
