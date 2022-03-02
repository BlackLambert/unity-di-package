using NUnit.Framework;

namespace SBaier.DI.Tests
{
    public class BindingValidationInstallerTests : InstallerTests<BindingValidationInstaller>
    {
        private TestBinder _binder;
		private BindingValidationInstaller _installer;

        [Test]
        public void BindingValidationInstaller_InstallBindings_BindsBindingValidator()
		{
			GivenADefaultSetup();
			WhenInstallBindingsIsCalledOn(_binder);
			ThenIsBound<InstantiationInfoValidator>(_binder);
		}

        [Test]
        public void BindingValidationInstaller_InstallBindings_BindsFromBindingValidator()
		{
            GivenADefaultSetup();
			WhenInstallBindingsIsCalledOn(_binder);
			ThenIsBound<InstanceCreationModeValidator>(_binder);
		}

		private void GivenADefaultSetup()
        {
            _binder = new TestBinder();
            _installer = new BindingValidationInstaller();
        }

        private void WhenInstallBindingsIsCalledOn(Binder binder)
        {
            _installer.InstallBindings(binder);
        }
    }
}