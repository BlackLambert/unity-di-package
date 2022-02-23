using UnityEngine;
using UnityEngine.SceneManagement;

namespace SBaier.DI.Examples.SceneDependencies
{
	public class CameraReceiver : MonoBehaviour, Injectable
	{
		public void Inject(Resolver resolver)
		{
			Camera cam = resolver.Resolve<Camera>();
			Scene scene = resolver.Resolve<Scene>();
			Debug.Log($"{nameof(CameraReceiver)} of Scene {scene.name} has resolved camera with name {cam.name}");
		}
	}
}
