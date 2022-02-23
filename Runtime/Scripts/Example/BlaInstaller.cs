using UnityEngine;

namespace SBaier.DI.Examples
{
	public class BlaInstaller : MonoInstaller
	{
		[SerializeField]
		private Bla _blaPrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<Factory<Bla>>().ToNew<BlaFactory>().AsSingle().WithArgument(_blaPrefab);
		}
	}
}
