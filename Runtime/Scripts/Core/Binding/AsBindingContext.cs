using System;

namespace SBaier.DI
{
    public class AsBindingContext : LazyModeBindingContext
    {
        public AsBindingContext(BindingArguments arguments) : base(arguments) { }

        public LazyModeBindingContext AsSingle()
        {
            _arguments.Binding.AmountMode = InstanceAmountMode.Single;
            return new LazyModeBindingContext(_arguments);
        }

        public LazyModeBindingContext PerRequest()
        {
            _arguments.Binding.AmountMode = InstanceAmountMode.PerRequest;
            return new LazyModeBindingContext(_arguments);
        }
    }
}

