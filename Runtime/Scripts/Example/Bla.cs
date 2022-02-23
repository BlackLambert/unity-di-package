using UnityEngine;

namespace SBaier.DI.Examples
{
    public class Bla : MonoBehaviour, Injectable
    {
        [SerializeField]
        private int _number = 12;
		private Baz _baz;

		public void Inject(Resolver resolver)
		{
			_baz = resolver.Resolve<Baz>();
		}

		public override string ToString()
		{
			return $"Starting Bla with {_number} and {_baz.Name}";
		}
	}
}
