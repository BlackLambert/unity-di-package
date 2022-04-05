using UnityEngine;

namespace SBaier.DI.Examples.Pooling
{
    public class Foo : MonoBehaviour
    {
        [field: SerializeField]
        public string Name { get; private set; } = "MyFoo";

		public override string ToString()
		{
			return $"Foo '{Name}'";
		}
	}
}
