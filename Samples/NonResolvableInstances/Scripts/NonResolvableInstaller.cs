using UnityEngine;

namespace SBaier.DI.Examples.NonResolvableInstances
{
	public class NonResolvableInstaller : MonoInstaller
	{
		[SerializeField]
		private Bar _barPrefab;

		public override void InstallBindings(Binder binder)
		{
			binder.BindToNewSelf<Foo>().WithArgument("My name is Foo. I am the {0}. of my kind");
			binder.BindComponent<Bar>().FromNewPrefabInstance(_barPrefab).AsNonResolvable();
		}
	}
}
