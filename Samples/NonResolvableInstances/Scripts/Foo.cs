using UnityEngine;

namespace SBaier.DI.Examples.NonResolvableInstances
{
    public class Foo : Injectable
    {
		private static int Count { get; set; } = 0;
		public string Name { get; private set; }

		public Foo()
		{
			Count++;
			Debug.Log($"Foo constructor call {Count}");
		}

		public void Inject(Resolver resolver)
		{
			Name = resolver.Resolve<string>();
			Name = string.Format(Name, Count);
		}
	}
}
