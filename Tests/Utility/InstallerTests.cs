using NUnit.Framework;

namespace SBaier.DI.Tests
{
    public class InstallerTests<TInstaller>
    {
        protected void ThenIsBound<TContract>(TestBinder binder)
        {
            Assert.IsTrue(binder.IsBound<TContract>(), GetNotBoundErrorText<TContract>());
        }

        private string GetNotBoundErrorText<TContract>()
		{
            return $"Required class {typeof(TContract)} is not bound by {typeof(TInstaller)}";
        }
    }
}
