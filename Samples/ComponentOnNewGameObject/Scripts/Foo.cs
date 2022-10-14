using UnityEngine;

namespace SBaier.DI.Examples
{
	public class Foo : MonoBehaviour, Injectable
	{
		public void Inject(Resolver resolver)
		{
			Debug.Log("Foo created");
		}
	}
}
