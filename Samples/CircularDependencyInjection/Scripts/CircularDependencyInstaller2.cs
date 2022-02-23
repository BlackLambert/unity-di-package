using UnityEngine;

namespace SBaier.DI.Examples.CircularDependencyInjection
{
	public class CircularDependencyInstaller2 : MonoInstaller
	{
		[SerializeField]
		private Baz _bazPrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<Foo>();
			binder.BindToNewSelf<Bar>();
			binder.BindComponent<Baz>().FromNewPrefabInstance(_bazPrefab).AsSingle().NonLazy();
		}
	}
}
