using System;

namespace SBaier.DI
{
    public class AsBindingContext : BindingContextBase
    {
        public AsBindingContext(BindingArguments arguments) : base(arguments) { }

		public AsBindingContext WithArgument<TArg>(TArg argument, IComparable iD = default)
        {
            BindingKey key = new BindingKey(typeof(TArg), iD);
            _arguments.Binding.Arguments.Add(key, argument);
            return this;
        }

        public void NonLazy()
		{
            _arguments.BindingStorage.AddToNonLazy(_binding);
        }
    }
}

