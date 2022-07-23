using System;

namespace SBaier.DI
{
    public class ArgumentsBindingContext : AsBindingContext
    {
        public ArgumentsBindingContext(BindingArguments arguments) : base(arguments) { }

        public ArgumentsBindingContext WithArgument<TArg>(TArg argument, IComparable iD = default)
        {
            BindingKey key = new BindingKey(typeof(TArg), iD);
            _arguments.Binding.Arguments.Add(key, argument);
            return this;
        }

        public void AsNonResolvable()
		{
            RemoveBindings();
            AddToNonLazy();
        }

        private void RemoveBindings()
		{
            foreach (BindingKey key in _arguments.Keys)
                _arguments.BindingStorage.RemoveBinding(key);
            _arguments.Keys.Clear();
        }

        private void AddToNonLazy()
        {
            _arguments.BindingStorage.AddToNonLazy(_arguments.Binding);
        }
    }
}

