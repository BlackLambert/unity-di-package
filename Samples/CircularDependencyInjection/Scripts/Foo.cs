using UnityEngine;

namespace SBaier.DI.Examples.CircularDependencyInjection
{
	public class Foo : Injectable
	{
		private Bar _bar;

		public void Inject(Resolver resolver)
		{
			_bar = resolver.Resolve<Bar>();
			Debug.Log(_bar);
		}
	}
}
