using UnityEngine;

namespace SBaier.DI
{
    public class BindingArguments
    {
        public Binding Binding { get; }
		public BindingStorage BindingStorage { get; }

		public BindingArguments(Binding binding, 
			BindingStorage bindingStorage)
		{
			Binding = binding;
			BindingStorage = bindingStorage;
		}
    }
}
