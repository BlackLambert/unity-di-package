namespace SBaier.DI
{
    public class ChildResolver : ResolverBase
	{
		private readonly Resolver _parent;
		private readonly Resolver _baseResolver;

		public ChildResolver(Resolver parent, Resolver baseResolver)
		{
			_parent = parent;
			_baseResolver = baseResolver;
		}

		public override bool IsResolvable(BindingKey key)
		{
			return _baseResolver.IsResolvable(key) || _parent.IsResolvable(key);
		}

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
			if (_baseResolver.IsResolvable(key))
				return _baseResolver.Resolve<TContract>(key);
			return _parent.Resolve<TContract>(key);
		}
	}
}
