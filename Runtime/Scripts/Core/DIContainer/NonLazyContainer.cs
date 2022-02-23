using System.Collections.Generic;

namespace SBaier.DI
{
    public class NonLazyContainer
    {
        private HashSet<Binding> _nonLazyBindings = new HashSet<Binding>();

        public void TryRemoving(Binding binding)
        {
            if (Has(binding))
                _nonLazyBindings.Remove(binding);
        }

        public bool Has(Binding binding)
        {
            return _nonLazyBindings.Contains(binding);
        }

        public void Add(Binding binding)
        {
            _nonLazyBindings.Add(binding);
        }

        public List<Binding> GetCopy()
        {
            return new List<Binding>(_nonLazyBindings);
        }
    }
}
