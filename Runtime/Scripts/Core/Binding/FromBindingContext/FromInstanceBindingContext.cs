namespace SBaier.DI
{
    public class FromInstanceBindingContext : BindingContextBase
    {
        public FromInstanceBindingContext(BindingArguments bindingArguments) : base(bindingArguments) 
        {
            _binding.InjectionAllowed = false;
        }

        public void WithoutInjection()
        {
            _binding.InjectionAllowed = false;
        }

        public ArgumentsForInstanceBindingContext WithInjection()
        {
            _binding.InjectionAllowed = true;
            return new ArgumentsForInstanceBindingContext(_arguments);
        }
    }
}
