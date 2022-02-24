using NUnit.Framework;
using UnityEngine;

namespace SBaier.DI.Tests
{
    public class QuitDetectorInstallerTests : InstallerTests<QuitDetectorInstaller>
    {
		private GameObject _gameObject;
		private QuitDetectorInstaller _installer;
		private TestBinder _binder;

        [TearDown]
        public void TearDown()
        {
            GameObject.Destroy(_gameObject);
        }

        [Test]
        public void ApplicationIsQuitting_QuitDetectorBoundToBinder()
        {
            GivenADefaultSetup();
            WhenInstallBindingsIsCalled();
            ThenIsBound<QuitDetector>(_binder);
        }

		private void GivenADefaultSetup()
		{
            _gameObject = new GameObject();
            _installer = new QuitDetectorInstaller(_gameObject);
            _binder = new TestBinder();
        }

        private void WhenInstallBindingsIsCalled()
		{
            _installer.InstallBindings(_binder);
        }
    }
}
