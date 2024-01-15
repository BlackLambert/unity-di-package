using System.Collections.Generic;

namespace SBaier.DI
{
    public class BindingComparer : IEqualityComparer<Binding>
    {
        public bool Equals(Binding x, Binding y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Binding obj)
        {
            return obj.GetHashCode();
        }
    }
}