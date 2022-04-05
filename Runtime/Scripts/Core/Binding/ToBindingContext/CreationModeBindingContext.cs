using System;

namespace SBaier.DI
{
    public class CreationModeBindingContext<TConcrete> 
    {
        protected BindingArguments _arguments;
        protected Binding _binding => _arguments.Binding;


        public CreationModeBindingContext(BindingArguments arguments)
        {
            _arguments = arguments;
            _binding.ConcreteType = typeof(TConcrete);
        }
        
        public FromInstanceBindingContext FromInstance(TConcrete instance)
        {
            _binding.CreationMode = InstanceCreationMode.FromInstance;
            _binding.ProvideInstanceFunction = () => instance;
            return new FromInstanceBindingContext(_arguments);
        }

        public ArgumentsBindingContext FromMethod(Func<TConcrete> create)
        {
            _binding.CreationMode = InstanceCreationMode.FromMethod;
            _binding.ProvideInstanceFunction = () => create();
            return new ArgumentsBindingContext(_arguments);
        }

        public ArgumentsBindingContext FromFactory()
        {
            _binding.CreationMode = InstanceCreationMode.FromFactory;
            return new ArgumentsBindingContext(_arguments);
        }
    }
}
