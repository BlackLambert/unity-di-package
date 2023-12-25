using UnityEngine;

namespace SBaier.DI
{
    public class MonoPool<TItem> : MonoPoolBase<TItem>, Pool<TItem> where TItem : Component
    {
        private Factory<TItem> _factory;
		protected Resolver _resolver;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_factory = resolver.Resolve<Factory<TItem>>();
			_resolver = resolver;
		}

		public TItem Request()
		{
			if (!HasStoredItem)
				return CreateInstance();
			return TakeItem(_resolver);
		}

		private TItem CreateInstance()
		{
			return _factory.Create();
		}
	}

	public class MonoPool<TItem, TArg> : MonoPoolBase<TItem>, Pool<TItem, TArg>, Injectable where TItem : Component
	{
		private Factory<TItem, TArg> _factory;
		private Resolver _resolver;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_factory = resolver.Resolve<Factory<TItem, TArg>>();
			_resolver = resolver;
		}

		public TItem Request(TArg arg)
		{
			if (!HasStoredItem)
				return CreateInstance(arg);
			return TakeItem(CreateResolver(arg));
		}

		private ArgumentsResolver CreateResolver(TArg arg)
		{
			ArgumentsResolver resolver = new ArgumentsResolver(_resolver);
			resolver.AddArgument(arg);
			return resolver;
		}

		private TItem CreateInstance(TArg arg)
		{
			return _factory.Create(arg);
		}
	}
}
