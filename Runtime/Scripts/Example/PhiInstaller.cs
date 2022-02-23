using UnityEngine;

namespace SBaier.DI.Examples
{
	public class PhiInstaller : MonoInstaller
	{
		[SerializeField]
		private Phi _phiPrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.Bind<IPhi>().ToComponent<Phi>().FromNewPrefabInstance(_phiPrefab).AsSingle();
		}
	}
}
