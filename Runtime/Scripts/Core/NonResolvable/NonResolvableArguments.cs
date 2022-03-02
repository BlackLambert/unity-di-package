using UnityEngine;

namespace SBaier.DI
{
    public class NonResolvableArguments
    {
        public NonResolvableInstanceInfo Info { get; }
        public BindingStorage BindingStorage { get; }

        public NonResolvableArguments(NonResolvableInstanceInfo info,
            BindingStorage bindingStorage)
        {
            Info = info;
            BindingStorage = bindingStorage;
        }
    }
}
