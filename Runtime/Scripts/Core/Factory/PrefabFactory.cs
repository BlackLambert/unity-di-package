using System;
using UnityEngine;

namespace SBaier.DI
{
	public abstract class PrefabFactoryBase<TPrefab> : Injectable where TPrefab : Component
	{
		private GameObjectInjector _injector;
		private TPrefab _prefab;

		protected Resolver BaseResolver { get; private set; }

		public virtual void Inject(Resolver resolver)
		{
			_injector = resolver.Resolve<GameObjectInjector>();
			_prefab = resolver.Resolve<TPrefab>();
			BaseResolver = resolver;
		}

		protected TPrefab CreateInstance(Resolver resolver)
		{
			bool formerActiveState = _prefab.gameObject.activeSelf;
			_prefab.gameObject.SetActive(false);
			TPrefab result = GameObject.Instantiate(_prefab);
			_injector.InjectIntoContextHierarchy(result.transform, resolver);
			result.gameObject.SetActive(formerActiveState);
			_prefab.gameObject.SetActive(formerActiveState);
			return result;
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
