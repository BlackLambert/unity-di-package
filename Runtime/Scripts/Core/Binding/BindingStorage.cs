using System;

namespace SBaier.DI
{
    public interface BindingStorage
    {
        public void AddBinding<TContract>(Binding binding, IComparable iD = default);
        public void AddToNonLazy(Binding binding);
    }
}
