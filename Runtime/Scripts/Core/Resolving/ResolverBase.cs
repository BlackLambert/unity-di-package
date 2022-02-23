using System;

namespace SBaier.DI
{
	public abstract class ResolverBase : Resolver
	{
		public TContract Resolve<TContract>()
		{
			return Resolve<TContract>((IComparable)default);
		}

		public TContract Resolve<TContract>(IComparable iD)
		{
			BindingKey key = CreateKey<TContract>(iD);
			return Resolve<TContract>(key);
		}

		public TContract Resolve<TContract>(BindingKey bindingKey)
		{
			return DoResolve<TContract>(bindingKey);
		}

		public TContract ResolveOptional<TContract>()
		{
			return ResolveOptional<TContract>((IComparable)default);
		}

		public TContract ResolveOptional<TContract>(IComparable iD)
		{
			BindingKey key = CreateKey<TContract>(iD);
			return ResolveOptional<TContract>(key);
		}

		public TContract ResolveOptional<TContract>(BindingKey bindingKey)
		{
			return IsResolvable(bindingKey) ? DoResolve<TContract>(bindingKey) : default;
		}

		public abstract bool IsResolvable(BindingKey key);

		protected abstract TContract DoResolve<TContract>(BindingKey key);

		protected BindingKey CreateKey<TContract>(IComparable iD)
		{
			return new BindingKey(typeof(TContract), iD);
		}
	}
}
