using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class NonLazyContainer
    {
        public IReadOnlyCollection<InstantiationInfo> InstanceInfos => _nonLazyInstanceInfos;
        private HashSet<InstantiationInfo> _nonLazyInstanceInfos = new HashSet<InstantiationInfo>();

        public void TryRemoving(InstantiationInfo binding)
        {
            if (Has(binding))
                _nonLazyInstanceInfos.Remove(binding);
        }

        public bool Has(InstantiationInfo binding)
        {
            return _nonLazyInstanceInfos.Contains(binding);
        }

        public void Add(InstantiationInfo binding)
        {
            _nonLazyInstanceInfos.Add(binding);
        }

		internal void Clear()
		{
            _nonLazyInstanceInfos.Clear();
        }
	}
}
