using System;
using System.Collections.Generic;

namespace SBaier.DI
{
    public class ArgumentsResolver : ResolverBase
    {
        private Resolver _baseResolver;
        private Dictionary<BindingKey, object> _arguments;

        public ArgumentsResolver(Resolver context, int size = 0)
		{
            _baseResolver = context;
            _arguments = new Dictionary<BindingKey, object>(size);
        }

        public void AddArgument<TContract>(TContract argument, IComparable iD = default)
		{
            _arguments.Add(CreateKey<TContract>(iD), argument);
        }

        public void AddArguments(Dictionary<BindingKey, object> arguments)
		{
			foreach (KeyValuePair<BindingKey, object> pair in arguments)
				_arguments.Add(pair.Key, pair.Value);
		}

        public void Clear()
        {
	        _arguments.Clear();
        }

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
			return _arguments.ContainsKey(key) ?
				(TContract)_arguments[key] :
				_baseResolver.Resolve<TContract>(key);
		}

		public override bool IsResolvable(BindingKey key)
		{
			return _arguments.ContainsKey(key) || _baseResolver.IsResolvable(key);
		}
	}
}
