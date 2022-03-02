using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class BindingContextBase
    {
        protected BindingArguments _arguments;
        protected Binding _binding => _arguments.Binding;
        protected BindingStorage _bindingStorage => _arguments.BindingStorage;
        protected HashSet<BindingKey> _keys => _arguments.Keys;

        public BindingContextBase(BindingArguments arguments)
        {
            _arguments = arguments;
        }
    }
}
