using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI.Examples.SceneDependencies
{
	public class CameraReceiver : MonoBehaviour, Injectable
	{
		private Camera _cam;
		private Scene _scene;

		public void Inject(Resolver resolver)
		{
			_cam = resolver.Resolve<Camera>();
			_scene = resolver.Resolve<Scene>();
		}

        private void OnEnable()
        {
			Debug.Log($"OnEnable: {nameof(CameraReceiver)} of Scene {_scene.name} has resolved camera with name {_cam.name}");
		}

		private void OnDisable()
		{
			Debug.Log($"OnDisable: {nameof(CameraReceiver)} of Scene {_scene.name} has resolved camera with name {_cam.name}");
		}
	}
}
