using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.DI.Examples.Pooling
{
    public class BarCreator : MonoBehaviour, Injectable
    {
		public static int Amount { get; set; } = 0;

		[SerializeField]
        private Button _addButton;
		[SerializeField]
		private Button _removeButton;
		[SerializeField]
		private Transform _hook;

		private Foo _foo;
		private Pool<Bar, Bar.Arguments> _pool;

		private Queue<Bar> _bars = new Queue<Bar>();
		private bool HasBar => _bars.Count > 0;

		public void Inject(Resolver resolver)
		{
			_pool = resolver.Resolve<Pool<Bar, Bar.Arguments>>();
			_foo = resolver.Resolve<Foo>();
		}

		private void OnEnable()
		{
			_addButton.onClick.AddListener(AddBar);
			_removeButton.onClick.AddListener(RemoveBar);
			UpdateInteractable();
		}

		private void OnDisable()
		{
			_addButton.onClick.RemoveListener(AddBar);
			_removeButton.onClick.RemoveListener(RemoveBar);
			StoreBars();
		}

		private void AddBar()
		{
			Amount++;
			Bar bar = _pool.Request(new Bar.Arguments(_foo, Amount));
			_bars.Enqueue(bar);
			bar.transform.SetParent(_hook, false);
			UpdateInteractable();
		}

		private void StoreBars()
		{
			while (HasBar)
				RemoveBar();
		}

		private void RemoveBar()
		{
			if (!HasBar)
				return;
			_pool.Return(_bars.Dequeue());
			UpdateInteractable();
		}

		private void UpdateInteractable()
		{
			_removeButton.interactable = HasBar;
		}
	}
}
