using UnityEngine;

namespace SBaier.DI
{
	public class MonoPool<TItem> : MonoPoolBase<TItem>, Pool<TItem>, Pool<TItem, Transform> where TItem : Component
    {
        private Factory<TItem, Transform> _factory;
		private Resolver _resolver;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_factory = resolver.Resolve<Factory<TItem, Transform>>();
			_resolver = resolver;
		}

		public TItem Request()
		{
			return Request(null);
		}

		public TItem Request(Transform parent)
		{
			return !HasStoredItem ? _factory.Create(parent) : TakeItem(_resolver);
		}
    }

	public class MonoPool<TItem, TArg> : MonoPoolBase<TItem>, Pool<TItem, TArg>, Pool<TItem, TArg, Transform> where TItem : Component
	{
		private const int _argumentsCount = 1;
		
		private Factory<TItem, TArg, Transform> _factory;
		private Resolver _baseResolver;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_factory = resolver.Resolve<Factory<TItem, TArg, Transform>>();
			_baseResolver = resolver;
		}

		public TItem Request(TArg arg)
		{
			return !HasStoredItem ? _factory.Create(arg, null) : TakeItem(CreateResolver(arg));
		}

		public TItem Request(TArg arg, Transform parent)
		{
			return !HasStoredItem ? _factory.Create(arg, parent) : TakeItem(CreateResolver(arg));
		}

		private ArgumentsResolver CreateResolver(TArg arg)
		{
			ArgumentsResolver resolver = new ArgumentsResolver(_baseResolver, _argumentsCount);
			resolver.AddArgument(arg);
			return resolver;
		}
	}
}
