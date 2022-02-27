using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public class FromInstanceBindingContext : BindingContextBase
    {

        public FromInstanceBindingContext(BindingArguments bindingArguments) : base(bindingArguments) { }

        public AsBindingContext WithInjection()
        {
            _binding.InjectionAllowed = false;
            return new AsBindingContext(_arguments);
        }

        public AsBindingContext WithoutInjection()
        {
            _binding.InjectionAllowed = false;
            return new AsBindingContext(_arguments);
        }
    }
}
