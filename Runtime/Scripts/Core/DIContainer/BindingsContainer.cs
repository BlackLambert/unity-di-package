using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class BindingsContainer
    {
        private readonly Dictionary<BindingKey, Binding> _bindings = new Dictionary<BindingKey, Binding>();

        public void AddBinding<TContract>(Binding binding, IComparable iD = null)
        {
            BindingKey key = CreateKey<TContract>(iD);
            AddBinding(key, binding);
        }

        public void AddBinding(BindingKey key, Binding binding)
        {
            ValidateNotBound(key);
            _bindings.Add(key, binding);
        }

        public Binding GetBinding(BindingKey key)
        {
            ValidateBindingExists(key);
            return _bindings[key];
        }

        public IEnumerable<Binding> GetBindings()
        {
            return _bindings.Values;
        }

        public bool HasBinding(BindingKey key)
        {
            return _bindings.ContainsKey(key);
        }

        private BindingKey CreateKey<TContract>(IComparable iD)
        {
            Type contract = typeof(TContract);
            return new BindingKey(contract, iD);
        }

        private void ValidateBindingExists(BindingKey key)
        {
            if (!HasBinding(key))
                throw new MissingBindingException($"There is no Binding for Contract {key}");
        }

        private void ValidateNotBound(BindingKey key)
        {
            if (HasBinding(key))
                throw new AlreadyBoundException();
        }
    }
}
