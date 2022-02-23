using UnityEngine;

namespace SBaier.DI.Examples
{
    public class Phi : MonoBehaviour, IPhi, Injectable
    {
        [SerializeField]
        private string _name = "Default";

		private PhiSettings _settings;

		public string Name => $"{_name} ({_settings.Num})";

		public void Inject(Resolver resolver)
		{
			_settings = resolver.Resolve<PhiSettings>();
		}
	}
}
