using UnityEngine;

namespace SBaier.DI
{
    public abstract class BindingContextBase
    {
        protected BindingArguments _arguments;
        protected Binding _binding => _arguments.Binding;
        protected BindingStorage _bindingStorage => _arguments.BindingStorage;

        public BindingContextBase(BindingArguments arguments)
        {
            _arguments = arguments;
        }
    }
}
