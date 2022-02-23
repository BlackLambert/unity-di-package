using UnityEngine;

namespace SBaier.DI.Examples
{
    public class Bar: IBar, Injectable
    {
        private Baz _baz;

		public Baz Baz => _baz;

		public void Inject(Resolver context)
        {
            _baz = context.Resolve<Baz>();
            Debug.Log($"Bar: Baz {_baz.Name}");
        }
    }
}
