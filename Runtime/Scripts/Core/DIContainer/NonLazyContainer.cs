using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class NonLazyContainer
    {
        public IReadOnlyCollection<Binding> Bindings => _nonLazyBindings;
        private HashSet<Binding> _nonLazyBindings = new HashSet<Binding>(new BindingComparer());
        private HashSet<Binding> _created = new HashSet<Binding>(new BindingComparer());

        public void TryAddToCreated(Binding binding)
        {
            if(_nonLazyBindings.Contains(binding) && !_created.Contains(binding))
                _created.Add(binding);
        }

        public bool ShallCreate(Binding binding)
        {
            return _nonLazyBindings.Contains(binding) && !_created.Contains(binding);
        }

        public void Add(Binding binding)
        {
            _nonLazyBindings.Add(binding);
        }

		internal void Clear()
		{
            _nonLazyBindings.Clear();
            _created.Clear();
        }
	}
}
