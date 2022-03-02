using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class NonResolvableInstanceInfo : InstantiationInfo
    {
        public Type ConcreteType { get; set; }
        public InstanceCreationMode CreationMode { get; set; }
        public Func<object> ProvideInstanceFunction { get; set; }
        public bool InjectionAllowed { get; set; }
        public Dictionary<BindingKey, object> Arguments { get; } =
            new Dictionary<BindingKey, object>();

        public NonResolvableInstanceInfo(Type concreteType)
        {
            ConcreteType = concreteType;
            CreationMode = InstanceCreationMode.Undefined;
            ProvideInstanceFunction = null;
            InjectionAllowed = true;
        }
    }
}
