using System.Collections.Generic;

namespace SBaier.DI
{
	public class CircularDependencyDetector : ResolverBase
	{
		private HashSet<BindingKey> resolveStack = new HashSet<BindingKey>();
		private Resolver _baseResolver;

		public CircularDependencyDetector(Resolver context)
		{
			_baseResolver = context;
		}

		public override bool IsResolvable(BindingKey key)
		{
			return _baseResolver.IsResolvable(key);
		}

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
			ValidateIsNoCircularDependeny(key);
			resolveStack.Add(key);
			TContract result = _baseResolver.Resolve<TContract>(key);
			resolveStack.Remove(key);
			return result;
		}

		private void ValidateIsNoCircularDependeny(BindingKey key)
		{
			if (resolveStack.Contains(key))
			{
				resolveStack.Clear();
				throw new CircularDependencyException(key);
			}
		}
	}
}
