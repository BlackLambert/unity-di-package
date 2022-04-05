using UnityEngine;

namespace SBaier.DI.Examples.Pooling
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		private Camera _camera;
		[SerializeField]
		private Foo _fooPrefab;
		[SerializeField]
		private Bar _barPrefab;
		[SerializeField]
		private Transform _poolHook;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<Foo>>().ToNew<PrefabFactory<Foo>>().WithArgument(_fooPrefab);
			binder.Bind<Factory<Bar, Bar.Arguments>>().ToNew<PrefabFactory<Bar, Bar.Arguments>>().WithArgument(_barPrefab);
			binder.Bind<Pool<Foo>>().ToNew<MonoPool<Foo>>().WithArgument(_poolHook).AsSingle();
			binder.Bind<Pool<Bar, Bar.Arguments>>().ToNew<MonoPool<Bar, Bar.Arguments>>().WithArgument(_poolHook).AsSingle();
		}
	}
}
