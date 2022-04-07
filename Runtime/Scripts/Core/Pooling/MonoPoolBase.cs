using UnityEngine;

namespace SBaier.DI
{
    public abstract class MonoPoolBase<TItem> : Injectable where TItem : Component
	{
		private GameObjectInjector _injector;
		private GameObjectDeactivator _deactivator;
		private MonoPoolCache _cache;
		private TItem _prefab;
		private int _prefabHash;

		protected bool HasStoredItem => _cache.HasObjects(_prefabHash);

		public virtual void Inject(Resolver resolver)
		{
			_injector = resolver.Resolve<GameObjectInjector>();
			_deactivator = resolver.Resolve<GameObjectDeactivator>();
			_prefab = resolver.Resolve<TItem>();
			_cache = resolver.Resolve<MonoPoolCache>();
			_prefabHash = _prefab.GetHashCode();
		}

		protected TItem TakeItem(Resolver resolver)
		{
			TItem item = _cache.Take<TItem>(_prefabHash);
			_injector.InjectIntoContextHierarchy(item.transform, resolver);
			item.gameObject.SetActive(true);
			return item;
		}

		public void Return(TItem item)
		{
			item.gameObject.SetActive(false);
			_deactivator.Deactivate(item.transform);
			_cache.Store(_prefabHash, item);
		}
	}
}
