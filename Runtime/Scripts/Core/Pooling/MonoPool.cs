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
		private ArgumentsResolver _resolver;

		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_factory = resolver.Resolve<Factory<TItem, TArg, Transform>>();
			_resolver = new ArgumentsResolver(resolver, _argumentsCount);
		}

		public TItem Request(TArg arg)
		{
			return !HasStoredItem ? _factory.Create(arg, null) : TakeItem(PrepareResolver(arg));
		}

		public TItem Request(TArg arg, Transform parent)
		{
			return !HasStoredItem ? _factory.Create(arg, parent) : TakeItem(PrepareResolver(arg));
		}

		private ArgumentsResolver PrepareResolver(TArg arg)
		{
			_resolver.Clear();
			_resolver.AddArgument(arg);
			return _resolver;
		}
	}
}
