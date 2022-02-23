using UnityEngine;

namespace SBaier.DI.Examples
{
	public class CameraInstaller : MonoInstaller
	{
		[SerializeField] private Camera _camera;

		public override void InstallBindings(Binder binder)
		{
			binder.BindToSelf<Camera>().FromInstanceAsSingle(_camera);
			binder.Bind<Sprite>().ToObject<Sprite>().FromResources("Sprites/Planet").AsSingle().NonLazy();
		}
	}
}
