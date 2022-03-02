using UnityEngine;

namespace SBaier.DI
{
	public class NonResolvableContext
	{
		private BindingArguments _arguments;

		public NonResolvableContext(BindingArguments arguments)
		{
			_arguments = arguments;
			_arguments.BindingStorage.AddToNonLazy(arguments.Binding);
		}

		public CreationModeBindingContext<TConcrete> Of<TConcrete>()
		{
			return new CreationModeBindingContext<TConcrete>(_arguments);
		}

		public FromNewBindingContext<TConcrete> OfNew<TConcrete>() where TConcrete : new()
		{
			return new FromNewBindingContext<TConcrete>(_arguments);
		}

		public ComponentCreationModeBindingContext<TConcrete> OfComponent<TConcrete>() where TConcrete : Component
		{
			return new ComponentCreationModeBindingContext<TConcrete>(_arguments);
		}

		public ObjectCreationModeBindingContext<TConcrete> OfObject<TConcrete>() where TConcrete : UnityEngine.Object
		{
			return new ObjectCreationModeBindingContext<TConcrete>(_arguments);
		}
	}
}
