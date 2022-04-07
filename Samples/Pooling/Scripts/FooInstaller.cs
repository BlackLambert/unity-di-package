using UnityEngine;

namespace SBaier.DI.Examples.Pooling
{
    public class FooInstaller : MonoInstaller
    {
        [SerializeField]
        private Foo _foo;
		[SerializeField]
		private Bar _barPrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_foo);
			binder.Bind<Factory<Bar, Bar.Arguments>>().ToNew<PrefabFactory<Bar, Bar.Arguments>>().WithArgument(_barPrefab);
			binder.Bind<Pool<Bar, Bar.Arguments>>().ToNew<MonoPool<Bar, Bar.Arguments>>().WithArgument(_barPrefab).AsSingle();
		}
	}
}
