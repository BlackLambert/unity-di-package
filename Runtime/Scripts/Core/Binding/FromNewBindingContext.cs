namespace SBaier.DI
{

	public class FromNewBindingContext<TConcrete> : FromBindingContext where TConcrete : new()
	{
		public FromNewBindingContext(BindingArguments arguments) : base(arguments)
		{
			_binding.ConcreteType = typeof(TConcrete);
			_binding.CreationMode = InstanceCreationMode.FromNew;
			_binding.CreateInstanceFunction = () => new TConcrete();
		}
	}
}
