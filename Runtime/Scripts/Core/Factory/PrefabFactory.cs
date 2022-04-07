using UnityEngine;

namespace SBaier.DI
{
	public abstract class PrefabFactoryBase<TPrefab> : Injectable where TPrefab : Component
	{
		private GameObjectInjector _injector;
		private TPrefab _prefab;
		private bool _initialPrefabActiveState;
		protected Resolver BaseResolver { get; private set; } 

		public virtual void Inject(Resolver resolver)
		{
			_injector = resolver.Resolve<GameObjectInjector>();
			_prefab = resolver.Resolve<TPrefab>();
			_initialPrefabActiveState = _prefab.gameObject.activeSelf;
			BaseResolver = resolver;
		}

		protected TPrefab CreateInstance(Resolver resolver)
		{
			_prefab.gameObject.SetActive(false);
			TPrefab instance = GameObject.Instantiate(_prefab);
			_prefab.gameObject.SetActive(_initialPrefabActiveState);
			_injector.InjectIntoContextHierarchy(instance.transform, resolver);
			instance.gameObject.SetActive(true);
			return instance;
		}
	}

	public class PrefabFactory<TPrefab> : PrefabFactoryBase<TPrefab>, Factory<TPrefab> where TPrefab : Component
	{
		public TPrefab Create()
		{
			return CreateInstance(BaseResolver);
		}
	}

	public class PrefabFactory<TPrefab, TArg> : PrefabFactoryBase<TPrefab>, Factory<TPrefab, TArg> where TPrefab : Component
	{
		public TPrefab Create(TArg arg)
		{
			ArgumentsResolver resolver = new ArgumentsResolver(BaseResolver);
			resolver.AddArgument(arg);
			return CreateInstance(resolver);
		}
	}
}
