using UnityEngine;

namespace SBaier.DI.Examples
{
	public class PhiReceiver : MonoBehaviour, Injectable
	{
		private IPhi _phi;

		public void Inject(Resolver resolver)
		{
			_phi = resolver.Resolve<IPhi>();
		}

		private void Start()
		{
			Debug.Log($"Phi name: {_phi.Name}");
		}
	}
}
