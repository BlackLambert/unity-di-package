using System;
using UnityEngine;
using Object = UnityEngine.Object;

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

		protected TPrefab CreateInstance(Resolver resolver, Transform parent = null)
		{
			bool formerActiveState = _prefab.gameObject.activeSelf;

			try
			{
				_prefab.gameObject.SetActive(false);
				TPrefab result = Object.Instantiate(_prefab, parent);
				_injector.InjectIntoContextHierarchy(result.transform, resolver);
				result.gameObject.SetActive(formerActiveState);
				_prefab.gameObject.SetActive(formerActiveState);
				return result;
			}
			catch (Exception exception)
			{
				Debug.LogError($"Failed to create an instance of {_prefab.name}");
				throw;
			}
			finally
			{
				_prefab.gameObject.SetActive(formerActiveState);
			}
		}
	}

	public class PrefabFactory<TPrefab> : 
		PrefabFactoryBase<TPrefab>, 
		Factory<TPrefab>, 
		Factory<TPrefab, Transform> where TPrefab : Component
	{
		public TPrefab Create()
		{
			return CreateInstance(BaseResolver);
		}

		public TPrefab Create(Transform parent)
		{
			return CreateInstance(BaseResolver, parent);
		}
	}

	public class PrefabFactory<TPrefab, TArg> : 
		PrefabFactoryBase<TPrefab>, 
		Factory<TPrefab, TArg>,
		Factory<TPrefab, TArg, Transform> where TPrefab : Component
	{
		private const int _argumentsCount = 1;
		
		private ArgumentsResolver _resolver;
		
		public override void Inject(Resolver resolver)
		{
			base.Inject(resolver);
			_resolver = new ArgumentsResolver(BaseResolver, _argumentsCount);
		}

		public TPrefab Create(TArg arg)
		{
			return CreateInstance(PrepareResolver(arg));
		}

		public TPrefab Create(TArg arg, Transform parent)
		{
			return CreateInstance(PrepareResolver(arg), parent);
		}

		private Resolver PrepareResolver(TArg arg)
		{
			_resolver.Clear();
			_resolver.AddArgument(arg);
			return _resolver;
		}
	}
}
