using UnityEngine;

namespace SBaier.DI.Examples.Pooling
{
    public class FooInstaller : MonoInstaller
    {
        [SerializeField]
        private Foo _foo;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_foo);
		}
	}
}
