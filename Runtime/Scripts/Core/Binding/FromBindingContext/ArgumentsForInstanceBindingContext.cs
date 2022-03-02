using System;

namespace SBaier.DI
{
    public class ArgumentsForInstanceBindingContext : BindingContextBase
    {
        public ArgumentsForInstanceBindingContext(BindingArguments arguments) : base(arguments) { }

        public ArgumentsForInstanceBindingContext WithArgument<TArg>(TArg argument, IComparable iD = default)
        {
            BindingKey key = new BindingKey(typeof(TArg), iD);
            _arguments.Binding.Arguments.Add(key, argument);
            return this;
        }
    }
}

