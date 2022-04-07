using UnityEngine;

namespace SBaier.DI.Examples.Pooling
{
	public class SceneInstaller : MonoInstaller
	{
		[SerializeField]
		private Foo _fooPrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<Foo>>().ToNew<PrefabFactory<Foo>>().WithArgument(_fooPrefab);
			binder.Bind<Pool<Foo>>().ToNew<MonoPool<Foo>>().WithArgument(_fooPrefab).AsSingle();
		}
	}
}
