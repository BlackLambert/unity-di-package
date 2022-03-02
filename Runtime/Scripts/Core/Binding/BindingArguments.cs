using System.Collections.Generic;

namespace SBaier.DI
{
    public class BindingArguments
    {
		public HashSet<BindingKey> Keys { get; } = new HashSet<BindingKey>();
        public Binding Binding { get; }
		public BindingStorage BindingStorage { get; }

		public BindingArguments(
			Binding binding, 
			BindingStorage bindingStorage)
		{
			Binding = binding;
			BindingStorage = bindingStorage;
		}
    }
}
