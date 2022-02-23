using UnityEngine;

namespace SBaier.DI.Examples.NonResolvableInstances
{
    public class Bar : MonoBehaviour, Injectable
    {
        private Foo _foo;

		public void Inject(Resolver resolver)
		{
			_foo = resolver.Resolve<Foo>();
		}

		private void Start()
		{
			Debug.Log($"Bar | Foo name: '{_foo.Name}'");
		}
	}
}
