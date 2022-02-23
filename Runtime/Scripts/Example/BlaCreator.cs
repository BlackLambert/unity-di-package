using UnityEngine;

namespace SBaier.DI.Examples
{
    public class BlaCreator : MonoBehaviour, Injectable
    {
		private Camera _cam;
		private Sprite _sprite;
		private Factory<Bla> _factory;

		public void Inject(Resolver resolver)
		{
			_factory = resolver.Resolve<Factory<Bla>>();
			_cam = resolver.Resolve<Camera>();
			_sprite = resolver.Resolve<Sprite>();
		}

		private void Start()
		{
			Bla bla = _factory.Create();
			bla.transform.SetParent(transform);
			Debug.Log(bla.ToString());
			Debug.Log($"The Cam is {_cam}");
			Debug.Log($"The Sprite is called {_sprite.name}");
		}
	}
}
