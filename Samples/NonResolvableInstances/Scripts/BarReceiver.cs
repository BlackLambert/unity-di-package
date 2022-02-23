using UnityEngine;

namespace SBaier.DI.Examples.NonResolvableInstances
{
	public class BarReceiver : MonoBehaviour, Injectable
	{
		public void Inject(Resolver resolver)
		{
			try
			{
				resolver.Resolve<Bar>();
			}
			catch(MissingBindingException)
			{
				Debug.Log($"{nameof(Bar)} is non resolvable.");
			}
		}
	}
}
