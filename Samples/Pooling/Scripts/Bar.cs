using UnityEngine;

namespace SBaier.DI.Examples.Pooling
{
	public class Bar : MonoBehaviour, Injectable
	{
		private Foo _foo;
		private int _number;

		public void Inject(Resolver resolver)
		{
			Arguments args = resolver.Resolve<Arguments>();
			_foo = args.Foo;
			_number = args.Num;
		}

		public override string ToString()
		{
			return $"Bar of {_foo.ToString()} with number {_number}.";
		}

		public class Arguments
		{
			public Foo Foo { get; private set; }
			public int Num { get; private set; }

			public Arguments(Foo foo, int num)
			{
				Foo = foo;
				Num = num;
			}
		}
	}
}
