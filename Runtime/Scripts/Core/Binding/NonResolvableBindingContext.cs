using UnityEngine;

namespace SBaier.DI
{
	public class NonResolvableBindingContext : BindingContextBase
    {
		public NonResolvableBindingContext(BindingArguments arguments) : base(arguments) { }

		public ToBindingContext<TConcrete> Of<TConcrete>()
		{
			return new ToBindingContext<TConcrete>(_arguments);
		}

		public FromNewBindingContext<TConcrete> OfNew<TConcrete>() where TConcrete : new()
		{
			return new FromNewBindingContext<TConcrete>(_arguments);
		}

		public ToComponentBindingContext<TConcrete> OfComponent<TConcrete>() where TConcrete : Component
		{
			return new ToComponentBindingContext<TConcrete>(_arguments);
		}
	}
}
