using System.Collections;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class MonoPoolBase<TItem> : Injectable where TItem : Component
	{
		private GameObjectInjector _injector;
		private GameObjectContextsReseter _reseter;
		private MonoPoolCache _cache;
		private TItem _prefab;
		private ObjectActivator _objectActivator;
		private int _prefabHash;
		private MonoBehaviour _coroutineHelper;

		protected bool HasStoredItem => _cache.HasObjects(_prefabHash);

		public virtual void Inject(Resolver resolver)
		{
			_injector = resolver.Resolve<GameObjectInjector>();
			_reseter = resolver.Resolve<GameObjectContextsReseter>();
			_prefab = resolver.Resolve<TItem>();
			_cache = resolver.Resolve<MonoPoolCache>();
			_objectActivator = resolver.Resolve<ObjectActivator>();
			_prefabHash = _prefab.GetHashCode();
			_coroutineHelper = resolver.Resolve<MonoBehaviour>();
		}

		protected TItem TakeItem(Resolver resolver)
		{
			TItem item = _cache.Take<TItem>(_prefabHash);
			_injector.InjectIntoContextHierarchy(item.transform, resolver);
			_objectActivator.Activate(item.gameObject);
			return item;
		}

		public void Return(TItem item)
		{
			GameObject gameObject = item.gameObject;
			_objectActivator.Disable(gameObject);
			_reseter.Reset(gameObject);
			_coroutineHelper.StartCoroutine(StoreDelayed(item));
		}

		private IEnumerator StoreDelayed(TItem item)
		{
			yield return null;
			_cache.Store(_prefabHash, item);
		}
	}
}
