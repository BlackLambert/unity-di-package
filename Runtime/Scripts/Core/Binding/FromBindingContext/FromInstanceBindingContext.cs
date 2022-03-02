namespace SBaier.DI
{
    public class FromInstanceBindingContext : ArgumentsForInstanceBindingContext
    {
        public FromInstanceBindingContext(BindingArguments bindingArguments) : base(bindingArguments) { }

        public void WithoutInjection()
        {
            _binding.InjectionAllowed = false;
        }
    }
}
