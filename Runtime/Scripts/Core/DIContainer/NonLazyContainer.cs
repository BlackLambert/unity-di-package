using System.Collections.Generic;

namespace SBaier.DI
{
    public class NonLazyContainer
    {
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

        public List<InstantiationInfo> GetCopy()
        {
            return new List<InstantiationInfo>(_nonLazyInstanceInfos);
        }
    }
}
