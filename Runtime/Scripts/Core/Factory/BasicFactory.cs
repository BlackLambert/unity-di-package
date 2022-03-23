namespace SBaier.DI
{
    public class BasicFactoryBase<TResult> : Injectable where TResult : new()
	{
		protected Resolver BaseResolver { get; private set; }

		public virtual void Inject(Resolver resolver)
		{
			BaseResolver = resolver;
		}

		protected TResult CreateInstance(Resolver resolver)
		{
			TResult instance = new TResult();
			if (instance is Injectable injectable)
				injectable.Inject(resolver);
			return instance;
		}
	}

    public class BasicFactory<TResult> : BasicFactoryBase<TResult>, Factory<TResult> where TResult : new()
	{
		public TResult Create()
		{
			return CreateInstance(BaseResolver);
		}
	}

	public class BasicFactory<TResult, TArg> : BasicFactoryBase<TResult>, Factory<TResult, TArg> where TResult : new()
	{
		public TResult Create(TArg arg)
		{
			ArgumentsResolver resolver = new ArgumentsResolver(BaseResolver);
			resolver.AddArgument(arg);
			return CreateInstance(resolver);
		}
	}
}
