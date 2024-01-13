using System.Collections.Generic;

namespace SBaier.DI
{
	public class CircularDependencyDetector : ResolverBase
	{
		private HashSet<BindingKey> resolveStack = new HashSet<BindingKey>(new BindingKeyComparer());
		private Resolver _baseResolver;

		public CircularDependencyDetector(Resolver baseResolver)
		{
			_baseResolver = baseResolver;
		}

		public override bool IsResolvable(BindingKey key)
		{
			return _baseResolver.IsResolvable(key);
		}

		protected override TContract DoResolve<TContract>(BindingKey key)
		{
			ValidateIsNoCircularDependency(key);
			resolveStack.Add(key);
			TContract result = _baseResolver.Resolve<TContract>(key);
			resolveStack.Remove(key);
			return result;
		}

		private void ValidateIsNoCircularDependency(BindingKey key)
		{
			if (resolveStack.Contains(key))
			{
				resolveStack.Clear();
				throw new CircularDependencyException(key);
			}
		}
	}
}
