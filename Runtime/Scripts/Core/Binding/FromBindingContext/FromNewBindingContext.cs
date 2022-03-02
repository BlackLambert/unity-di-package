namespace SBaier.DI
{

	public class FromNewBindingContext<TConcrete> : ArgumentsBindingContext where TConcrete : new()
	{
		public FromNewBindingContext(BindingArguments arguments) : base(arguments)
		{
			_binding.ConcreteType = typeof(TConcrete);
			_binding.CreationMode = InstanceCreationMode.FromNew;
			_binding.ProvideInstanceFunction = () => new TConcrete();
		}
	}
}
