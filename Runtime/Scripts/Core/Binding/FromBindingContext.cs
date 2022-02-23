namespace SBaier.DI
{
    public class FromBindingContext : BindingContextBase
    {
        public FromBindingContext(BindingArguments arguments) : base(arguments) { }

		public AsBindingContext AsSingle()
        {
            _arguments.Binding.AmountMode = InstanceAmountMode.Single;
            return new AsBindingContext(_arguments);
        }

        public AsBindingContext PerRequest()
        {
            _arguments.Binding.AmountMode = InstanceAmountMode.PerRequest;
            return new AsBindingContext(_arguments);
        }
    }
}

