using UnityEngine;

namespace SBaier.DI
{
    public class LazyModeBindingContext : BindingContextBase
    {
        public LazyModeBindingContext(BindingArguments arguments) : base(arguments) { }

        public void NonLazy()
        {
            _arguments.BindingStorage.AddToNonLazy(_binding);
        }
    }
}
