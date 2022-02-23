namespace SBaier.DI
{
	public class BindingValidationInstaller : Installer
	{
		public void InstallBindings(Binder binder)
		{
			binder.Bind<FromBindingValidator>().ToNew<FromBindingValidatorImpl>();
			binder.BindToNewSelf<BindingValidator>();
		}
	}
}