using UnityEngine;

namespace SBaier.DI.Examples
{
    public class BarNonLazyInstaller : MonoInstaller, Injectable
    {
        [SerializeField]
        private Baz _baz;

		public override void InstallBindings(Binder binder)
        {
            binder.Bind<Bar>().And<IBar>("IBar").ToNew<Bar>().AsSingle().NonLazy();
            binder.BindInstance(_baz).NonLazy();
        }

		void Injectable.Inject(Resolver resolver)
		{
            _baz.FromerBaz = resolver.ResolveOptional<Baz>();
        }
	}
}