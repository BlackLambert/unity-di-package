using UnityEngine;

namespace SBaier.DI.Examples.SceneDependencies
{
	public class CameraInstaller : MonoInstaller
	{
		[SerializeField]
		private Camera _camera;

		public override void InstallBindings(Binder binder)
		{
			binder.BindInstance(_camera);
		}
	}
}
