using System;

namespace SBaier.DI
{
    public class ToBindingContext<TConcrete> : BindingContextBase
    {
        public ToBindingContext(BindingArguments bindingArguments) : base(bindingArguments)
        {
            _binding.ConcreteType = typeof(TConcrete);
        }
        
        public AsBindingContext FromInstanceAsSingle(TConcrete instance)
        {
            _binding.CreationMode = InstanceCreationMode.FromInstance;
            _binding.CreateInstanceFunction = () => instance;
            _binding.InjectionAllowed = false;
            _binding.AmountMode = InstanceAmountMode.Single;
            return new AsBindingContext(_arguments);
        }

        public FromBindingContext FromMethod(Func<TConcrete> create)
        {
            _binding.CreationMode = InstanceCreationMode.FromMethod;
            _binding.CreateInstanceFunction = () => create();
            return new FromBindingContext(_arguments);
        }

        public FromBindingContext FromFactory()
        {
            _binding.CreationMode = InstanceCreationMode.FromFactory;
            return new FromBindingContext(_arguments);
        }
    }
}
