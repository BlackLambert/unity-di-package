using System.Collections.Generic;

namespace SBaier.DI
{
    public class SingleInstancesContainer
    {
        private readonly Dictionary<Binding, object> _singleInstances = new Dictionary<Binding, object>();

        public bool Has(Binding binding)
        {
            return _singleInstances.ContainsKey(binding);
        }

        public TContract Get<TContract>(Binding binding)
        {
            ValidateHasSingleInstance(binding);
            return (TContract)_singleInstances[binding];
        }

        public void Store<TContract>(Binding key, TContract instance)
        {
            ValidateHasNoSingleInstance(key);
            _singleInstances.Add(key, instance);
        }

        private void ValidateHasSingleInstance(Binding key)
        {
            if (!Has(key))
                throw new MissingSingleInstanceException();
        }

        private void ValidateHasNoSingleInstance(Binding key)
        {
            if (Has(key))
                throw new MissingSingleInstanceException();
        }
    }
}
