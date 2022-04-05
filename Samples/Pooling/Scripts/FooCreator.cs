using System;
using UnityEngine;
using UnityEngine.UI;

namespace SBaier.DI.Examples.Pooling
{
    public class FooCreator : MonoBehaviour, Injectable
    {
        [SerializeField]
        private Button _addButton;
        [SerializeField]
        private Button _removeButton;
		[SerializeField]
		private Transform _fooHook;

        private Pool<Foo> _pool;
		private Foo _currentFoo;
		private bool HasFoo => _currentFoo != null;

		public void Inject(Resolver resolver)
		{
            _pool = resolver.Resolve<Pool<Foo>>();
        }

		private void OnEnable()
		{
			_addButton.onClick.AddListener(AddFoo);
			_removeButton.onClick.AddListener(RemoveFoo);
			UpdateInteractable();
		}

		private void OnDisable()
		{
			_addButton.onClick.RemoveListener(AddFoo);
			_removeButton.onClick.RemoveListener(RemoveFoo);
		}

		private void AddFoo()
		{
			if (HasFoo)
				return;
			_currentFoo = _pool.Request();
			_currentFoo.transform.SetParent(_fooHook, false);
			UpdateInteractable();
		}

		private void RemoveFoo()
		{
			if (!HasFoo)
				return;
			_pool.Return(_currentFoo);
			_currentFoo = null;
			UpdateInteractable();
		}

		private void UpdateInteractable()
		{
			_addButton.interactable = !HasFoo;
			_removeButton.interactable = HasFoo;
		}
	}
}
