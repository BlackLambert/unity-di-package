using System;
using System.Collections.Generic;
using UnityEngine;

namespace SBaier.DI
{
    public abstract class MonoPoolBase<TItem> : IDisposable, Injectable where TItem : Component
	{
		private GameObjectInjector _injector;
		private GameObjectDeactivator _deactivator;
		private Transform _hook;

		protected Queue<TItem> _items = new Queue<TItem>();
		protected bool HasStoredItem => _items.Count > 0;

		public virtual void Inject(Resolver resolver)
		{
			_injector = resolver.Resolve<GameObjectInjector>();
			_deactivator = resolver.Resolve<GameObjectDeactivator>();
			_hook = resolver.ResolveOptional<Transform>();
		}

		public void Dispose()
		{
			foreach (TItem item in _items)
				GameObject.Destroy(item.gameObject);
			_items.Clear();
		}

		protected TItem TakeItem(Resolver resolver)
		{
			TItem item = _items.Dequeue();
			_injector.InjectIntoContextHierarchy(item.transform, resolver);
			item.gameObject.SetActive(true);
			return item;
		}

		public void Return(TItem item)
		{
			item.transform.SetParent(_hook, false);
			item.gameObject.SetActive(false);
			_deactivator.Deactivate(item.transform);
			_items.Enqueue(item);
		}
	}
}
