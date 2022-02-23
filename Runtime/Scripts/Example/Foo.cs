using UnityEngine;

namespace SBaier.DI.Examples
{
    public class Foo : MonoBehaviour, Injectable
    {
        private Bar _bar;
        private IBar _ibar;
        private Baz _baz;

        public void Inject(Resolver context)
        {
            _bar = context.Resolve<Bar>();
            _ibar = context.Resolve<IBar>("IBar");
            _baz = context.Resolve<Baz>();
            Debug.Log($"Foo: Bar {_bar}");
            Debug.Log($"Foo: Bar ({_bar}) and IBar ({_ibar}) are the same? {_ibar.Equals(_bar)}");
            Debug.Log($"Foo: Baz with name {_baz.Name}");
        }
    }
}