using UnityEngine;

namespace SBaier.DI.Examples.Pooling
{
	public class BarInstaller : MonoInstaller
	{
		[SerializeField]
		private Bar _bar;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_bar);
		}
	}
}
