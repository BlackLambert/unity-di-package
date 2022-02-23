using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class BasicInstanceResolver : ResolverBase
    {
        private Dictionary<BindingKey, object> _instances =
            new Dictionary<BindingKey, object>();

        public void Add<TContract>(TContract instance, IComparable iD = default)
		{
            BindingKey key = CreateKey<TContract>(iD);
            _instances.Add(key, instance);
        }

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
            return (TContract)_instances[key];
        }

		public override bool IsResolvable(BindingKey key)
		{
            return _instances.ContainsKey(key);
        }
	}
}

