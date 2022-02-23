namespace SBaier.DI
{
    public class Bootstrapper
    {
        public Resolver Resolver { get; private set; }

        public Bootstrapper()
        {
            Resolver = CreateResolver();
        }

		private Resolver CreateResolver()
        {
            BasicInstanceResolver result = new BasicInstanceResolver();
            BasicDIContext context = new BasicDIContext();
            (context as Injectable).Inject(CreateBasicDIContextResolver());
            result.Add(context);
            return result;
        }

		public Resolver CreateBasicDIContextResolver()
		{
            BasicInstanceResolver result = new BasicInstanceResolver();
            BindingValidator validator = new BindingValidator();
            (validator as Injectable).Inject(CreateBindingValidatorResolver());
            result.Add(validator);
            result.Add(new GameObjectInjector());
            result.Add(new DIContainers(new BindingsContainer(), new SingleInstancesContainer(), new NonLazyContainer()));
            result.Add(new DIInstanceFactory());
            return result;
        }

        private Resolver CreateBindingValidatorResolver()
		{
            BasicInstanceResolver result = new BasicInstanceResolver();
            result.Add<FromBindingValidator>(new FromBindingValidatorImpl());
            return result;
        }
    }
}

